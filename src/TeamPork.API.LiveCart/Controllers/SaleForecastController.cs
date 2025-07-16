using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeamPork.LiveCart.Core.Services.LiveCart.ML.SaleForecast.Interface;
using TeamPork.LiveCart.Core.Utils;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Model.LiveCart.ML.SaleForecast;

namespace TeamPork.API.LiveCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForecastController : ControllerBase
    {
        private readonly ISalesForecastService _forecastService;
        private readonly AppDbContext dbContext;

        public ForecastController(ISalesForecastService forecastService, AppDbContext dbContext)
        {
            _forecastService = forecastService;
            this.dbContext = dbContext;
        }

        [HttpGet("predict-upcoming-month-from-latest-invoice")]
        [Authorize]
        public ActionResult PredictUpcomingMonthFromLatestInvoice([FromQuery] bool fromBusiness = false)
        {
            if (!long.TryParse(User.FindFirst(fromBusiness ? ClaimTypes.PrimaryGroupSid : ClaimTypes.NameIdentifier)?.Value, out var id))
                throw new UnauthorizedAccessException();
            var today = DateTime.Today;

            var invoices = dbContext.Invoices
                .Where(i => i.OrderedAt <= DateOnly.FromDateTime(today) && i.Status == "paid" && (fromBusiness ? i.BusinessSeqId == id : i.UserSeqId == id))
                .Select(i => new { i.OrderedAt, i.Total })
                .AsEnumerable()
                .Select(i => (i.OrderedAt.ToDateTime(TimeOnly.MinValue), i.Total))
                .ToList();

            var data = SaleForecastUtils.GenerateMonthlyRevenue(invoices);
            if (data.Count < 2)
                return BadRequest("Not enough data for prediction.");

            var last = data[^1];
            var prev = data[^2];

            var nextMonth = (int)(last.Month == 12 ? 1 : last.Month + 1);
            var nextYear = (int)(last.Month == 12 ? last.Year + 1 : last.Year);

            var input = new SaleForecastData
            {
                Year = nextYear,
                Month = nextMonth,
                Lag1 = last.TotalRevenue,
                Lag2 = prev.TotalRevenue
            };

            var prediction = _forecastService.PredictUpcomingMonthFromLatestInvoice(input, id, fromBusiness);

            return Ok(new
            {
                NextMonth = $"{nextYear}-{nextMonth:D2}",   
                PredictedRevenue = Math.Round(prediction.PredictedRevenue, 2)
            });
        }
        [HttpGet("predict/upcoming-months")]
        [Authorize]
        public ActionResult PredictNext3Months([FromQuery] bool fromBusiness = false)
        {
            if (!long.TryParse(User.FindFirst(fromBusiness ? ClaimTypes.PrimaryGroupSid : ClaimTypes.NameIdentifier)?.Value, out var id))
                throw new UnauthorizedAccessException();

            var today = DateTime.Today;

            var invoices = dbContext.Invoices
                .Where(i => i.OrderedAt <= DateOnly.FromDateTime(today) && i.Status == "paid" && (fromBusiness ? i.BusinessSeqId == id : i.UserSeqId == id))
                .Select(i => new { i.OrderedAt, i.Total })
                .AsEnumerable()
                .Select(i => (i.OrderedAt.ToDateTime(TimeOnly.MinValue), i.Total))
                .ToList();

            var data = SaleForecastUtils.GenerateMonthlyRevenue(invoices);
            if (data.Count < 2)
                return BadRequest("Not enough data for prediction.");

            var forecasted = _forecastService.PredictUpcomingMonths(data, id, fromBusiness);

            return Ok(forecasted.Select(r => new
            {
                Year = r.year,
                Month = r.month,
                PredictedRevenue = Math.Round(r.predicted, 2)
            }));
        }
    }

}
