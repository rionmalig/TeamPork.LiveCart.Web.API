using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using System.Globalization;
using TeamPork.LiveCart.Core.Services.LiveCart.ML.SaleForecast.Interface;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.ML;
using TeamPork.LiveCart.Model.LiveCart.ML.SaleForecast;
using TeamPork.LiveCart.Model.LiveCart.ML.SaleForecast.Response;

namespace TeamPork.LiveCart.Core.Services.LiveCart.ML.SaleForecast
{
    public class SalesForecastService : ISalesForecastService
    {
        private readonly MLContext _mlContext = new();

        private string GetModelPath(long id, bool fromBusiness)
        {
            var dir = Path.Combine("Models", "Forecasts");
            Directory.CreateDirectory(dir);
            var type = fromBusiness ? "business" : "user";
            return Path.Combine(dir, $"{type}_{id}.zip");
        }

        public void TrainModelAsync(List<SaleForecastData> data, long id, bool fromBusiness)
        {
            var trainingData = _mlContext.Data.LoadFromEnumerable(data);

            var pipeline = _mlContext.Transforms
                .Concatenate("Features", "Year", "Month", "Lag1", "Lag2")
                .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: "TotalRevenue"));

            var model = pipeline.Fit(trainingData);

            var modelPath = GetModelPath(id, fromBusiness);
            using var fs = File.Create(modelPath);
            _mlContext.Model.Save(model, trainingData.Schema, fs);
        }

        public SaleForecastPrediction PredictUpcomingMonthFromLatestInvoice(SaleForecastData input, long id, bool fromBusiness)
        {
            var modelPath = GetModelPath(id, fromBusiness);

            if (!File.Exists(modelPath))
                throw new FileNotFoundException("Model not found. Train it first.");

            using var stream = File.OpenRead(modelPath);
            var model = _mlContext.Model.Load(stream, out _);
            var engine = _mlContext.Model.CreatePredictionEngine<SaleForecastData, SaleForecastPrediction>(model);

            return engine.Predict(input);
        }

        public List<(int year, int month, float predicted)> PredictUpcomingMonths(List<SaleForecastData> historicalData, long id, bool fromBusiness, int monthsToPredict = 3)
        {
            var modelPath = GetModelPath(id, fromBusiness);
            if (!File.Exists(modelPath))
                throw new FileNotFoundException("Model not found");

            using var stream = File.OpenRead(modelPath);
            var model = _mlContext.Model.Load(stream, out _);
            var engine = _mlContext.Model.CreatePredictionEngine<SaleForecastData, SaleForecastPrediction>(model);

            var last = historicalData[^1];
            var prev = historicalData[^2];

            float lag1 = last.TotalRevenue;
            float lag2 = prev.TotalRevenue;

            var current = DateTime.Now;
            int currentYear = current.Year;
            int currentMonth = current.Month;

            var results = new List<(int year, int month, float predicted)>();

            for (int i = 0; i < monthsToPredict; i++)
            {
                var input = new SaleForecastData
                {
                    Year = currentYear,
                    Month = currentMonth,
                    Lag1 = lag1,
                    Lag2 = lag2
                };

                var prediction = engine.Predict(input);
                results.Add((currentYear, currentMonth, prediction.PredictedRevenue));

                lag2 = lag1;
                lag1 = prediction.PredictedRevenue;

                if (currentMonth == 12)
                {
                    currentMonth = 1;
                    currentYear++;
                }
                else
                {
                    currentMonth++;
                }
            }

            return results;
        }
    }
}
