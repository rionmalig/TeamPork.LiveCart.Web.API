﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeamPork.LiveCart.Core.Services.Helper.Interface;
using TeamPork.LiveCart.Infrastructure.Data.Entities;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.Auth;
using TeamPork.LiveCart.Infrastructure.Data.Generic.Repository.Interface;
using TeamPork.LiveCart.Model.LiveCart;
using TeamPork.LiveCart.Model.LiveCart.Auth;
using TeamPork.LiveCart.Model.LiveCart.Auth.Request;
using TeamPork.LiveCart.Model.LiveCart.Auth.Response;

namespace TeamPork.LiveCart.Core.Jwt.Service
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IRepository<UserEntity, long> _userRepo;
        private readonly IRepository<UserRefreshTokenEntity, long> _refreshTokenRepo;
        private readonly IRepository<UserProfileEntity, long> _userProfileRepo;
        private readonly IRepository<BusinessProfileEntity, long> _businessProfileRepo;

        public JwtService(IConfiguration configuration, 
            IPasswordHasherService passwordHasher, 
            IMapper mapper,
            IRepository<UserEntity, long> userRepo, 
            IRepository<UserRefreshTokenEntity, long> refreshTokenRepo,
            IRepository<BusinessProfileEntity, long> businessprofileRepo,
            IRepository<UserProfileEntity, long> userProfileRepo

            )
        {
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _userRepo = userRepo;
            _refreshTokenRepo = refreshTokenRepo;

            _userProfileRepo = userProfileRepo;
            _businessProfileRepo = businessprofileRepo;
        }

        public async Task<LoginResponse?> Authenticate(long userId)
        {
            var user = await _userRepo
                 .GetByIdAsync( userId );

            if (user is null)
                return null;

            var refreshToken = await _refreshTokenRepo
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.UserSeqId == user.Id);

            if (refreshToken is not null)
                await _refreshTokenRepo.RemoveAsync(refreshToken);

            var response = await GenerateToken(user);

            return response;
        }

        public async Task<LoginResponse?> Authenticate(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return null;
           var user = await _userRepo
                .AsQueryable()
                .Include(u => u.Business)
                .FirstOrDefaultAsync(user => user.Username == request.Username || user.Email == request.Username);

            if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
                return null;

            var refreshToken = await _refreshTokenRepo
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.UserSeqId == user.Id);

            if (refreshToken is not null)
                await _refreshTokenRepo.RemoveAsync(refreshToken);

            var response = await GenerateToken(user);

            return response;
        }

        public async Task<LoginResponse?> ValidateRefreshToken(string token)
        {
            var refreshToken = await _refreshTokenRepo.AsQueryable()
                .FirstOrDefaultAsync(x => x.Token == token);
            if (refreshToken is null || refreshToken.Expirey < DateTime.UtcNow) 
                return null;

            await _refreshTokenRepo.RemoveAsync(refreshToken);
            
            var user = await _userRepo.AsQueryable()
                .Include(u => u.Business)
                .FirstOrDefaultAsync(u => u.Id == refreshToken.UserSeqId);
            if (user is null)
                return null;

            return await GenerateToken(user);
        }

        private async Task<LoginResponse> GenerateToken(UserEntity user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.PrimaryGroupSid, user.BusinessSeqId.ToString() ?? ""),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:AccessTokenExpirationMinutes");
            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var token = new JwtSecurityToken(issuer, 
                audience, 
                claims, 
                expires: tokenExpiryTimeStamp, 
                signingCredentials: creds);

            var accessToken = new JwtSecurityTokenHandler()
                .WriteToken(token);

            var refreshToken = await GenerateRefreshToken(user.Id);

            var userProfile = _mapper.Map<UserProfile>(await _userProfileRepo.GetByIdAsync(user.UserProfileSeqId ?? 0));
            var businesProfile = _mapper.Map<BusinessProfile>(await _businessProfileRepo.GetByIdAsync(user.Business?.BusinessProfileSeqId ?? 0));

            return new LoginResponse
            {
                AccessToken = accessToken,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
                RefreshToken = refreshToken.Token,

                Username = user.Username,
                Email = user.Email,

                UserProfile = userProfile,
                BusinessProfile = businesProfile
            };
        }
        private async Task<UserRefreshToken> GenerateRefreshToken(long userId)
        {
            var refreshTokenValidityDays = _configuration.GetValue<int>("JwtConfig:RefreshTokenExpirationDays");
            var refreshToken = new UserRefreshTokenEntity
            {
                Token = Guid.NewGuid().ToString(),
                Expirey = DateTime.UtcNow.AddDays(refreshTokenValidityDays),
                UserSeqId = userId,
            };
            await _refreshTokenRepo.AddAsync(refreshToken);
            return _mapper.Map<UserRefreshToken>(refreshToken);

        }
    }
}
