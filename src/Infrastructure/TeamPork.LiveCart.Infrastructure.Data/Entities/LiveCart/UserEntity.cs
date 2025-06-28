using System.ComponentModel.DataAnnotations.Schema;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart
{
    public class UserEntity : AuditEntity<long>
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }

        public long? UserProfileSeqId { get; set; }
        [ForeignKey("UserProfileSeqId")]
        public UserProfileEntity? UserProfile { get; set; }

        public long? BusinessSeqId { get; set; }
        [ForeignKey("BusinessSeqId")]
        public BusinessEntity? Business { get; set; }

        public BusinessEntity? OwnedBusiness { get; set; }

        public ICollection<InvoiceEntity>? Invoices { get; set; }
        public ICollection<ItemEntity>? Items { get; set; }
        public ICollection<CustomerEntity>? Customers { get; set; }
    }
}
