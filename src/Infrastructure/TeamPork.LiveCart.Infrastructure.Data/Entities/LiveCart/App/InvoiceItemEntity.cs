using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App
{
    public class InvoiceItemEntity : SyncedEntity<long>
    {
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public long InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public InvoiceEntity? Invoice { get; set; }
        public string? InvoiceClientId { get; set; }
        public long ItemId { get; set; }
        [ForeignKey("ItemId")]
        public ItemEntity? Item { get; set; }
        public string? ItemClientId { get; set; }
    }
}
