using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart
{
    public class BusinessInviteCodeEntity : PrimaryKey<long>
    {
        public required string Code { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8);

        public long BusinessSeqId { get; set; }
        [ForeignKey("BusinessSeqId")]
        public BusinessEntity? Business { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsRedeemed { get; set; } = false;

        public long? RedeemedByUserSeqId { get; set; }
        [ForeignKey("RedeemedByUserSeqId")]
        public UserEntity? RedeemedBy { get; set; }
    }
}
