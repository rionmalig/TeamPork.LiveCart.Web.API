using AutoMapper;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart;
using TeamPork.LiveCart.Model.LiveCart;

namespace TeamPork.LiveCart.Core.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, User>().ReverseMap();
        }
    }
}
