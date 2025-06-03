using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.Auth;
using TeamPork.LiveCart.Model.LiveCart.Auth;

namespace TeamPork.LiveCart.Core.AutoMapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<UserRefreshTokenEntity, UserRefreshToken>().ReverseMap();
        }
    }
}
