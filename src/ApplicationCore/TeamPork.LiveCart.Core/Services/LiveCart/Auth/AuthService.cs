using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Core.Jwt.Service;
using TeamPork.LiveCart.Core.Services.Helper;
using TeamPork.LiveCart.Core.Services.Helper.Interface;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart;
using TeamPork.LiveCart.Infrastructure.Data.Generic.Repository.Interface;
using TeamPork.LiveCart.Model.LiveCart.Auth.Request;
using TeamPork.LiveCart.Model.LiveCart.Auth.Response;

namespace TeamPork.LiveCart.Core.Services.LiveCart.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtService _jwtService;
        private readonly IPasswordHasherService _hasherService;
        private readonly IRepository<UserEntity, long> _userRepo;

        public AuthService(JwtService jwtService, IPasswordHasherService hasherService, IRepository<UserEntity, long> userRepo)
        {
            _jwtService = jwtService;
            _hasherService = hasherService;
            _userRepo = userRepo;
        }
        public async Task<LoginResponse?> Register(RegisterRequest registerRequest)
        {
            if (string.IsNullOrWhiteSpace(registerRequest.Username) || string.IsNullOrWhiteSpace(registerRequest.Email) || string.IsNullOrWhiteSpace(registerRequest.Password))
                return null;

            var detailsAlreadyExist = _userRepo
                .AsQueryable()
                .Any(user => user.Email == registerRequest.Email || user.Username == registerRequest.Username);

            if(detailsAlreadyExist)
                return null;

            var user = new UserEntity
            { 
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                PasswordHash = _hasherService.Hash(registerRequest.Password),
                UserRoleSeqId = 1   ,
                Active = true,
                CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                CreatedBy = 1
            };

            await _userRepo.AddAsync(user);

            var loginRequest = new LoginRequest
            {
                Username = registerRequest.Username,
                Password = registerRequest.Password,
            };

            return await _jwtService.Authenticate(loginRequest);
        }
    }
}
