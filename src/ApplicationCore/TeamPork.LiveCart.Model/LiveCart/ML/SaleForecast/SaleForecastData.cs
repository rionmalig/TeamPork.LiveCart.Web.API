using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPork.LiveCart.Model.LiveCart.ML.SaleForecast
{
    public class SaleForecastData
    {
        public float Year { get; set; }
        public float Month { get; set; }

        public float Lag1 { get; set; }
        public float Lag2 { get; set; }

        public float TotalRevenue { get; set; }
    }
}
