using AuthenticationforTheMemeoryGame.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthenticationforTheMemeoryGame.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(jwtSettings);

            
            if (string.IsNullOrEmpty(jwtSettings.SecretKey))
                throw new ArgumentNullException("JwtSettings:Key is missing");

            var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;// For development only
                options.SaveToken = true;// Save token in the AuthenticationProperties after a successful authorization
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Core validation logic
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                   // ClockSkew=TimeSpan.Zero // Eliminate delay when token is expire
                };
            });
        }
    }
}