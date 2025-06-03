using Microsoft.Extensions.DependencyInjection;
using TeamPork.LiveCart.Core.AutoMapper;

namespace TeamPork.LiveCart.Core.Configuration
{
    public static partial class Extension
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile).Assembly);
        }
    }
}
