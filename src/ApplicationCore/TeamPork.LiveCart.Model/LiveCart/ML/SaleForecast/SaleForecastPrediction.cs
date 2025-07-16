using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPork.LiveCart.Model.LiveCart.ML.SaleForecast
{
    public class SaleForecastPrediction
    {
        [ColumnName("Score")]
        public float PredictedRevenue { get; set; }
    }
}
