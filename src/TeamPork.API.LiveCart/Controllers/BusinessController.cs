using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Infrastructure.Data.Entities;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart;
using TeamPork.LiveCart.Model.LiveCart;

namespace TeamPork.API.LiveCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public BusinessController(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBusiness([FromBody] BusinessProfile businessProfile)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid user ID");

            var user = await appDbContext.Users.FindAsync(userId);
            if (user == null)
                return Unauthorized("Invalid User");



            var newBusinessProfileEntity = mapper.Map<BusinessProfileEntity>(businessProfile);
            await appDbContext.BusinessProfiles.AddAsync(newBusinessProfileEntity);
            await appDbContext.SaveChangesAsync();

            var newBusinessEntity = new BusinessEntity
            {
                OwnerSeqId = user.Id,
                BusinessProfileSeqId = newBusinessProfileEntity.Id,
            };
            await appDbContext.Businesses.AddAsync(newBusinessEntity);
            await appDbContext.SaveChangesAsync();


            user.BusinessSeqId = newBusinessEntity.Id;
            await appDbContext.SaveChangesAsync();

            return Ok(businessProfile);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody] BusinessProfile businessProfile)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid user ID");

            var user = await appDbContext.Users
                .Include(u => u.Business)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Business == null)
                return NotFound("Business profile not found");

            var businessProfileEntity = await appDbContext.BusinessProfiles.FindAsync(user.Business.BusinessProfileSeqId);

            mapper.Map(businessProfile, businessProfileEntity);
            await appDbContext.SaveChangesAsync();

            return Ok(businessProfile);
        }
    }
}
