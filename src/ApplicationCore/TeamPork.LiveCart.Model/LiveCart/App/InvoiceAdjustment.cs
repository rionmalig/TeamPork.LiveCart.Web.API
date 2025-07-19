using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.Abstract;

namespace TeamPork.LiveCart.Model.LiveCart.App
{
    public class InvoiceAdjustment : SyncedModel<long>
    {
        public required string Description { get; set; }
        public float Amount { get; set; }
        public bool IsPercentage { get; set; }
        public required string Type { get; set; }
        public long? InvoiceId { get; set; }
        public string? InvoiceClientId { get; set; }
    }
}
