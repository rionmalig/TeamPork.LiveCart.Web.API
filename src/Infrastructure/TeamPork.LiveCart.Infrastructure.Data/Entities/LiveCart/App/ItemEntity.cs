using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App
{
    public class ItemEntity : SyncedEntity<long>
    {
        public required string Name { get; set; }
        public string? Code { get; set; }
        [Precision(18, 2)]
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public bool IsPercentage { get; set; }
        public ICollection<InvoiceItemEntity>? InvoiceItems { get; set; }
    }
}
