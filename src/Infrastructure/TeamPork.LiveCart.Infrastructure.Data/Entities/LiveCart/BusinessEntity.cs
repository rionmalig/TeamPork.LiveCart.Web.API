using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart
{
    public class BusinessEntity : PrimaryKey<long>
    {
        public long OwnerSeqId { get; set; }
        [ForeignKey("OwnerSeqId")]
        public UserEntity? Owner { get; set; }

        public long? BusinessProfileSeqId { get; set; }
        [ForeignKey("BusinessProfileSeqId")]
        public BusinessProfileEntity? BusinessProfile { get; set; }

        public ICollection<UserEntity>? Members { get; set; }

        // Shared data
        public ICollection<InvoiceEntity>? Invoices { get; set; }
        public ICollection<CustomerEntity>? Customers { get; set; }
        public ICollection<ItemEntity>? Items { get; set; }
    }
}