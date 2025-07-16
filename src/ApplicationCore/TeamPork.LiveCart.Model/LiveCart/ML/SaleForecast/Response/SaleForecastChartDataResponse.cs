using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPork.LiveCart.Model.LiveCart.ML.SaleForecast.Response
{
    public class SaleForecastChartDataResponse
    {
        public List<string> Labels { get; set; } = [];
        public List<float> Totals { get; set; } = [];
        public int ForecastIndex { get; set; }
        public double R2Score { get; set; }
    }
}
