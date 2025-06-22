using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App
{
    public class InvoiceEntity : SyncedEntity<long>
    {
        public required DateOnly OrderedAt { get; set; }
        public required DateOnly DueAt { get; set; }
        public decimal Total { get; set; }
        public long CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerEntity? Customer { get; set; }
        public string? CustomerClientId { get; set; }
        public ICollection<InvoiceItemEntity>? InvoiceItems { get; set; }
    }
}
