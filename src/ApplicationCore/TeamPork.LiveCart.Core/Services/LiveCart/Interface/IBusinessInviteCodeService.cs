using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart;

namespace TeamPork.LiveCart.Core.Services.LiveCart.Interface
{
    public interface IBusinessInviteCodeService
    {
        Task<BusinessInviteCodeEntity> GenerateCodeAsync(long businessId);
        Task<BusinessProfileEntity?> RedeemCodeAsync(string code, long userId);
    }
}
