using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.Auth
{
    public class UserRefreshTokenEntity : PrimaryKey<long>
    {
        public required long UserSeqId { get; set; }
        [ForeignKey("UserSeqId")]
        public UserEntity? User { get; set; }
        public required string Token { get; set; }
        public required DateTime Expirey { get; set; }
    }
}
