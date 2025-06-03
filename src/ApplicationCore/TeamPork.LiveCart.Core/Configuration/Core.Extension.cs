using Microsoft.Extensions.DependencyInjection;
using TeamPork.LiveCart.Core.Jwt.Service;
using TeamPork.LiveCart.Core.Services.Helper;
using TeamPork.LiveCart.Core.Services.Helper.Interface;
using TeamPork.LiveCart.Core.Services.LiveCart.Auth;

namespace TeamPork.LiveCart.Core.Configuration
{
    public static partial class Extension
    {
        // Add Services Here!
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<JwtService>();
            services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
