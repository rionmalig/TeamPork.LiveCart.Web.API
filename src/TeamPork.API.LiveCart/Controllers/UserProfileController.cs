using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Infrastructure.Data.Entities;
using TeamPork.LiveCart.Model.LiveCart;

namespace TeamPork.API.LiveCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public UserProfileController(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfile userProfile, [FromServices] ILogger<UserProfileController> logger)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid user ID");

            var user = await appDbContext.Users.FindAsync(userId);
            if (user == null)
                return Unauthorized("Invalid User");

            var entity = mapper.Map<UserProfileEntity>(userProfile);
            await appDbContext.UserProfiles.AddAsync(entity);
            await appDbContext.SaveChangesAsync();

            user.UserProfileSeqId = entity.Id;
            await appDbContext.SaveChangesAsync();

            return Ok(userProfile);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfile updatedProfile)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid user ID");

            var user = await appDbContext.Users
                .Include(u => u.UserProfile)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.UserProfile == null)
                return NotFound("User profile not found");

            mapper.Map(updatedProfile, user.UserProfile);
            await appDbContext.SaveChangesAsync();

            return Ok(updatedProfile);
        }
    }
}
