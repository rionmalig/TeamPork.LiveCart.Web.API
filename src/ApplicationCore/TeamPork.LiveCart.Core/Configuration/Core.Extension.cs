using Microsoft.Extensions.DependencyInjection;
using TeamPork.LiveCart.Core.Generic.GenericODataService.Interface;
using TeamPork.LiveCart.Core.Generic.GenericODataService;
using TeamPork.LiveCart.Core.Jwt.Service;
using TeamPork.LiveCart.Core.Services.Helper;
using TeamPork.LiveCart.Core.Services.Helper.Interface;
using TeamPork.LiveCart.Core.Services.LiveCart.App;
using TeamPork.LiveCart.Core.Services.LiveCart.App.Interface;
using TeamPork.LiveCart.Core.Services.LiveCart.Auth;
using TeamPork.LiveCart.Infrastructure.Data.Generic.GenericSyncedService;
using TeamPork.LiveCart.Infrastructure.Data.Generic.GenericSyncedService.Interface;
using TeamPork.LiveCart.Core.Services.LiveCart.ML.SaleForecast.Interface;
using TeamPork.LiveCart.Core.Services.LiveCart.ML.SaleForecast;
using TeamPork.LiveCart.Core.Jobs.SaleForecast;
using TeamPork.LiveCart.Core.Services.LiveCart.Interface;
using TeamPork.LiveCart.Core.Services.LiveCart;

namespace TeamPork.LiveCart.Core.Configuration
{
    public static partial class Extension
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<JwtService>();
            services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISyncService, SyncService>();
            services.AddScoped<IBusinessInviteCodeService, BusinessInviteCodeService>();

            services.AddScoped<ISalesForecastService, SalesForecastService>();

            // Generic services niggas
            services.AddScoped(typeof(IGenericSyncedService<,>), typeof(GenericSyncedService<,>));
            services.AddScoped(typeof(IGenericOdataService<,,>), typeof(GenericOdataService<,,>));

            services.AddTransient<SaleForecastJobExecutor>();


        }
    }
}
