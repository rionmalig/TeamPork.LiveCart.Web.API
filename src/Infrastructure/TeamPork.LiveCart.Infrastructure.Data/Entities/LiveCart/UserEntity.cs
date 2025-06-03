using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart
{
    public class UserEntity : AuditEntity<long>
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public long UserRoleSeqId { get; set; }
        [ForeignKey("UserRoleSeqId")]
        public UserRoleEntity? UserRole { get; set; }
    }
}
