using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TeamPork.API.LiveCart.Configuration
{
    public class Orgins
    {
        public string? Host { get; set; }
        public bool UseHttp { get; set; } = false;
    }

    public static partial class Extension
    {
        public const string PolicyAllowed = nameof(PolicyAllowed);
        public const string SectionName = "Orgins";
        public const string SectionAllowed = $"{SectionName}:Allowd";

        public static void ConfigureOrgins(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrgins = configuration.GetSection(SectionAllowed).Get<Orgins[]>();

            if (allowedOrgins == null) return;

            List<string> expandedOrgins = [];

            foreach (var item in allowedOrgins)
            {
                if (item.UseHttp)
                    expandedOrgins.Add($"http://{item.Host}");
                expandedOrgins.Add($"https://{item.Host}");
            }

            services.AddCors(o => o.AddPolicy(PolicyAllowed, builder =>
            {
                builder.WithOrigins(expandedOrgins.ToArray())
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials() // Allow cookies/authentication if needed
                .WithExposedHeaders("Content-Disposition"); // Expose the Content-Disposition header
            }));
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var jwtConfig = configuration.GetSection("JwtConfig");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtConfig["Issuer"],
                    ValidAudience = jwtConfig["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtConfig["Secret"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
