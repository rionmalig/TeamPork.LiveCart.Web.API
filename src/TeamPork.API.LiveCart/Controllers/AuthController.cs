using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamPork.LiveCart.Core.Jwt.Service;
using TeamPork.LiveCart.Core.Services.LiveCart.Auth;
using TeamPork.LiveCart.Model.LiveCart.Auth.Request;
using TeamPork.LiveCart.Model.LiveCart.Auth.Response;

namespace TeamPork.API.LiveCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IAuthService _authService;

        public AuthController(JwtService jwtService, IAuthService authService)
        {
            _jwtService = jwtService;
            _authService = authService;
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _jwtService.Authenticate(loginRequest);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterRequest registerRequest)
        {
            var result = await _authService.Register(registerRequest);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Refresh")]
        public async Task<ActionResult<LoginResponse>> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (string.IsNullOrWhiteSpace(refreshRequest.Token))
                return BadRequest("Invalid Request Body");
            var result = await _jwtService.ValidateRefreshToken(refreshRequest.Token);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }
    }
}
