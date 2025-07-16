using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.LiveCart.ML.SaleForecast;

namespace TeamPork.LiveCart.Core.Utils
{
    public static class SaleForecastUtils
    {
        public static List<SaleForecastData> GenerateMonthlyRevenue(List<(DateTime date, float total)> rawData)
        {
            var grouped = rawData
                .GroupBy(x => new DateTime(x.date.Year, x.date.Month, 1))
                .ToDictionary(g => g.Key, g => g.Sum(x => x.total));

            var firstMonth = grouped.Keys.Min();
            var lastMonth = grouped.Keys.Max();

            var result = new List<SaleForecastData>();
            var current = new DateTime(firstMonth.Year, firstMonth.Month, 1);

            while (current <= lastMonth)
            {
                float revenue = grouped.TryGetValue(current, out var val) ? val : 0f;

                result.Add(new SaleForecastData
                {
                    Year = current.Year,
                    Month = current.Month,
                    TotalRevenue = revenue
                });

                current = current.AddMonths(1);
            }

            for (int i = 2; i < result.Count; i++)
            {
                result[i].Lag1 = result[i - 1].TotalRevenue;
                result[i].Lag2 = result[i - 2].TotalRevenue;
            }

            return result.Skip(2).ToList();
        }
    }
}
