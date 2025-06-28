using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract
{
    public class SyncedEntity<Tid> : PrimaryKey<Tid>
    {
        [Column(Order = 1)]
        public string? ClientId { get; set; }
        public long? UserSeqId { get; set; }
        [ForeignKey("UserSeqId")]
        public UserEntity? User { get; set; }
        public long? BusinessSeqId { get; set; }
        [ForeignKey("BusinessSeqId")]
        public BusinessEntity? Business { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
