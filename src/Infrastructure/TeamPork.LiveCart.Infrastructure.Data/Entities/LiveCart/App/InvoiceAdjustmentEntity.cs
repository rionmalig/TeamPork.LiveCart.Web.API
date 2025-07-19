using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App
{
    public class InvoiceAdjustmentEntity : SyncedEntity<long>
    {
        public required string Description { get; set; }
        public float Amount { get; set; }
        public bool IsPercentage { get; set; }
        public required string Type { get; set; }
        public long InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public InvoiceEntity? Invoice { get; set; }
        public string? InvoiceClientId { get; set; }
    }
}
