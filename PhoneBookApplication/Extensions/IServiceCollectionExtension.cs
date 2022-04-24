using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PhoneBookApplication.Core.Entities;
using PhoneBookApplication.Core.Services.InfrastructureServices;
using PhoneBookApplication.Infrastructure.Services;
using System;
using System.IO;
using System.Reflection;

namespace PhoneBookApplication.Extensions
{
    public static class IServiceCollectionExtension
    {

        public static void ResolveAPICors(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors(options => ConfigureCorsPolicy(options));

            CorsOptions ConfigureCorsPolicy(CorsOptions corsOptions)
            {
                string allowedHosts = config["Appsettings:AllowedHosts"];
                if (string.IsNullOrEmpty(allowedHosts))
                    corsOptions.AddPolicy("DenyAllHost",
                                      corsPolicyBuilder => corsPolicyBuilder
                                      .AllowAnyHeader()
                                      .WithMethods(new string[3] { "POST", "PATCH", "HEAD" })
                                     );
                else if (allowedHosts == "*")
                {
                    corsOptions.AddPolicy("AllowAll",
                                        corsPolicyBuilder => corsPolicyBuilder
                                        .AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        );
                }
                else
                {
                    string[] allowedHostArray;

                    if (!allowedHosts.Contains(","))
                        allowedHostArray = new string[1] { allowedHosts };
                    else
                        allowedHostArray = allowedHosts.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    corsOptions.AddPolicy("AllowAll",
                                      corsPolicyBuilder => corsPolicyBuilder
                                      .AllowAnyHeader()
                                      .WithOrigins(allowedHostArray)
                                      .WithMethods(new string[3] { "POST", "PATCH", "HEAD" })
                                     );
                }
                return corsOptions;
            }
        }

        public static void ResolveCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IPhoneBookQueryCommand<Contact>, PhoneBookQueryCommand<Contact>>();

        }
    }
}
