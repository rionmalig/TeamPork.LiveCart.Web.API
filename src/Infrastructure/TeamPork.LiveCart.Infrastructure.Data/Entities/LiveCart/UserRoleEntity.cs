using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.Abstract;

namespace TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart
{
    public class UserRoleEntity : AuditEntity<long>
    {
        public ICollection<UserEntity>? Users { get; set; }
    }
}
