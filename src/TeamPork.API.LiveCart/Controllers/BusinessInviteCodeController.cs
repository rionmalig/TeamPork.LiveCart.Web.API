using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeamPork.LiveCart.Core.Jwt.Service;
using TeamPork.LiveCart.Core.Services.LiveCart.Interface;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart;
using TeamPork.LiveCart.Model.LiveCart;
using TeamPork.LiveCart.Model.LiveCart.Auth.Response;

namespace TeamPork.API.LiveCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessInviteCodeController : ControllerBase
    {
        private readonly IBusinessInviteCodeService _service;
        private readonly IMapper mapper;
        private readonly JwtService jwtService;

        public BusinessInviteCodeController(IBusinessInviteCodeService service, IMapper mapper, JwtService jwtService)
        {
            _service = service;
            this.mapper = mapper;
            this.jwtService = jwtService;
        }

        [HttpPost("generate")]
        [Authorize]
        public async Task<IActionResult> GenerateInvite()
        {

            if (!long.TryParse(User.FindFirst(ClaimTypes.PrimaryGroupSid)?.Value, out var businessId))
                return Unauthorized("Invalid business ID");
            var code = await _service.GenerateCodeAsync(businessId);
            return Ok(new { code = code.Code });
        }

        [HttpPost("redeem/{code}")]
        [Authorize]
        public async Task<ActionResult<LoginResponse>> RedeemInvite([FromRoute] string code)
        {
            if (!long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                return Unauthorized("Invalid user ID");
            var result = await _service.RedeemCodeAsync(code, userId);
            if (result == null) return BadRequest("Invalid or already redeemed code.");

            var loginResponse = await  jwtService.Authenticate(userId);

            return Ok(loginResponse);
        }
    }

}
