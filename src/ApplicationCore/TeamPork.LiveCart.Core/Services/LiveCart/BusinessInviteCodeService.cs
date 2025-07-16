using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Core.Services.LiveCart.Interface;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart;

namespace TeamPork.LiveCart.Core.Services.LiveCart
{
    public class BusinessInviteCodeService : IBusinessInviteCodeService
    {
        private readonly AppDbContext _context;

        public BusinessInviteCodeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BusinessInviteCodeEntity> GenerateCodeAsync(long businessId)
        {
            var code = new BusinessInviteCodeEntity
            {
                Code = Guid.NewGuid().ToString("N").Substring(0, 8),
                BusinessSeqId = businessId,
            };

            _context.BusinessInviteCodes.Add(code);
            await _context.SaveChangesAsync();
            return code;
        }

        public async Task<BusinessProfileEntity?> RedeemCodeAsync(string code, long userId)
        {
            var invite = await _context.BusinessInviteCodes
                .Include(i => i.Business)
                .FirstOrDefaultAsync(i => i.Code == code && !i.IsRedeemed);

            if (invite == null) return null;

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            var businessProfile = await _context.BusinessProfiles
                .FindAsync(invite.Business?.BusinessProfileSeqId);
            if(businessProfile == null) return null;

            invite.IsRedeemed = true;
            invite.RedeemedByUserSeqId = userId;

            user.BusinessSeqId = invite.BusinessSeqId;

            await _context.SaveChangesAsync();
            return businessProfile;
        }
    }

}
