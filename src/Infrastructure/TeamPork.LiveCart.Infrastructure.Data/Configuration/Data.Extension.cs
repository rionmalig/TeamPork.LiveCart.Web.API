using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;
using TeamPork.LiveCart.Infrastructure.Data.Generic.Repository.Interface;
using TeamPork.LiveCart.Infrastructure.Data.Generic.Repository;

namespace TeamPork.LiveCart.Infrastructure.Data.Configuration
{
    public static class Extension
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings");

            services.AddDbContext<AppDbContext>(c =>
                c.UseNpgsql(connectionStrings["livecart-postgre-database"],
                x => x.MigrationsAssembly("TeamPork.LiveCart.Infrastructure")));
        }

        // Add Services
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        }
    }

}
