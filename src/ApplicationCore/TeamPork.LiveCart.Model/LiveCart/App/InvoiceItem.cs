using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Model.Abstract;

namespace TeamPork.LiveCart.Model.LiveCart.App
{
    public class InvoiceItem : SyncedModel<long>
    {
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public long? InvoiceId { get; set; }
        public string? InvoiceClientId { get; set; }
        public long? ItemId { get; set; }
        public string? ItemClientId { get; set; }
    }
}
