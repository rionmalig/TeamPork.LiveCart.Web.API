using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Core.Services.LiveCart.ML.SaleForecast.Interface;
using TeamPork.LiveCart.Core.Utils;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Model.LiveCart.ML.SaleForecast;

namespace TeamPork.LiveCart.Core.Jobs.SaleForecast
{
    public class SaleForecastJobExecutor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SaleForecastJobExecutor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task RetrainAllUsers()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SaleForecastJobExecutor>>();

            var records = await db.Invoices
                .Where(i => i.Status == "paid")
                .Select(i => new { id = i.UserSeqId ?? i.BusinessSeqId, fromBusiness = i.BusinessSeqId != null })
                .Distinct()
                .ToListAsync();

            logger.LogInformation($"record count: {records.Count}");
            var today = DateTime.Today;

            await Parallel.ForEachAsync(records, async (record, ct) =>
            {
                try
                {
                    using var userScope = _scopeFactory.CreateScope();
                    var forecastService = userScope.ServiceProvider.GetRequiredService<ISalesForecastService>();
                    var scopedDb = userScope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var fromBusiness = record.fromBusiness;
                    var id = record.id;

                    var invoices = await scopedDb.Invoices
                        .Where(i => i.OrderedAt <= DateOnly.FromDateTime(today) && i.Status == "paid" && (fromBusiness ? i.BusinessSeqId == id : i.UserSeqId == id))
                        .Select(i => new { i.OrderedAt, i.Total })
                        .ToListAsync(ct);

                   
                    var data = SaleForecastUtils.GenerateMonthlyRevenue(
                        invoices.Select(i => (i.OrderedAt.ToDateTime(TimeOnly.MinValue), i.Total)).ToList()
                    );
                    for (int i = 0; i < data.Count; i++)
                    {
                        var item = data[i];
                        logger.LogInformation($"Data {i+1} {item.Year} {item.Month}, {item.Lag1} {item.Lag2} {item.TotalRevenue}");
                    }


                    if (data.Count < 3)
                    {
                        logger.LogInformation("Skipping ID {Id} due to insufficient data", id);
                        return;
                    }
                    logger.LogInformation("Training for ID {Id}", id);

                    forecastService.TrainModelAsync(data, id ?? 0, fromBusiness);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Training failed for ID {Id}", record.id);
                }
            });
        }
        
    }
}
