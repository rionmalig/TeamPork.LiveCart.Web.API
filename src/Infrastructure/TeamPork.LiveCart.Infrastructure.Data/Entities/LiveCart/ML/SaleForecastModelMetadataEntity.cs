using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.ML
{
    public class SaleForecastModelMetadataEntity : PrimaryKey<long>
    {
        public long? UserSeqId { get; set; }
        [ForeignKey("UserSeqId")]
        public UserEntity? User { get; set; }

        public long? BusinessSeqId { get; set; }
        [ForeignKey("BusinessSeqId")]
        public BusinessEntity? Business { get; set; }

        public string ModelPath { get; set; } = default!;
        public DateTime LastTrainedAt { get; set; }
        public DateOnly TrainingMinDate { get; set; }
        public float? TotalMin { get; set; }
        public float? TotalMax { get; set; }
        public double? R2Score { get; set; }
    }
}
