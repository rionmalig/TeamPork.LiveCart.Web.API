using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.OData.ModelBuilder;
using TeamPork.LiveCart.Core.Generic.GenericODataService.Interface;
using TeamPork.LiveCart.Core.Generic.GenericODataService;
using TeamPork.LiveCart.Core.Jwt.Service;
using TeamPork.LiveCart.Core.Services.Helper.Interface;
using TeamPork.LiveCart.Core.Services.Helper;
using TeamPork.LiveCart.Core.Services.LiveCart.App.Interface;
using TeamPork.LiveCart.Core.Services.LiveCart.App;
using TeamPork.LiveCart.Core.Services.LiveCart.Auth;
using TeamPork.LiveCart.Core.Services.LiveCart.ML.SaleForecast.Interface;
using TeamPork.LiveCart.Core.Services.LiveCart.ML.SaleForecast;
using TeamPork.LiveCart.Infrastructure.Data.Generic.GenericSyncedService.Interface;
using TeamPork.LiveCart.Infrastructure.Data.Generic.GenericSyncedService;
using TeamPork.LiveCart.Model.LiveCart;
using TeamPork.LiveCart.Model.LiveCart.App;
using Hangfire.PostgreSql;
using TeamPork.LiveCart.Core.Jobs.SaleForecast;

namespace TeamPork.API.LiveCart.Configuration
{
    public static partial class HangfireExtension
    {
        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings");
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionStrings["livecart-hangfire-postgre-database"]))
            );

            services.AddHangfireServer();
        }

        public static void RegisterRecurringJobs(IRecurringJobManager jobManager)
        {
            jobManager.AddOrUpdate<SaleForecastJobExecutor>(
                "daily-sales-forecast-retraining",
                executor => executor.RetrainAllUsers(),
                Cron.Daily
            );
        }
    }
}
