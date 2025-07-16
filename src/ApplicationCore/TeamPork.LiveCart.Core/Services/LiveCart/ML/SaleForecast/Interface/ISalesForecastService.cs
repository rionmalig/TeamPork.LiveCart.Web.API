using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.LiveCart.ML.SaleForecast;
using TeamPork.LiveCart.Model.LiveCart.ML.SaleForecast.Response;

namespace TeamPork.LiveCart.Core.Services.LiveCart.ML.SaleForecast.Interface
{
    public interface ISalesForecastService
    {
        void TrainModelAsync(List<SaleForecastData> data, long id, bool fromBusiness);
        SaleForecastPrediction PredictUpcomingMonthFromLatestInvoice(SaleForecastData input, long id, bool fromBusiness);
        List<(int year, int month, float predicted)> PredictUpcomingMonths(List<SaleForecastData> historicalData, long id, bool fromBusiness, int monthsToPredict = 3);
    }
}
