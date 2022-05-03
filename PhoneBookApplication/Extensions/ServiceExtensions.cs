using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PhoneBookApplication.Core.Entities;
using PhoneBookApplication.Infrastructure.Data.DatabaseContexts;
using System;
using System.Text;

namespace PhoneBookApplication.Extensions
{
    public static class ServiceExtensions
    {
        //Extend configurations here in order not to congest the startup
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<AppUser>(u => { u.User.RequireUniqueEmail = true; });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<PhoneBookDbContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            //this configuration comes from appsettings.json
            var jwtSettings = configuration.GetSection("Jwt");

            // this comes from saved secret KEY on comand prompt run as Admin
            //setx KEY "GUID values" /M
            //(/M means it must be a system variable(Environment Variabl) not a local variable)

            var key = Environment.GetEnvironmentVariable("KEY");

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }
    }
}
