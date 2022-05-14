using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PhoneBookApplication.Core.Services.InfrastructureServices;
using PhoneBookApplication.Extensions;
using PhoneBookApplication.Infrastructure.Data.DatabaseContexts;
using PhoneBookApplication.Infrastructure.Extensions;
using PhoneBookApplication.Infrastructure.Helpers;
using PhoneBookApplication.Infrastructure.Services;
using PhoneBookApplication.Infrastructure.Services.Implementation;
using PhoneBookApplication.Infrastructure.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PhoneBookDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"))
            );

            //services.ResolveAPICors(Configuration);
            services.ResolveCoreServices();
            services.ResolveInfrastructureServices();

            services.ConfigureHttpCacheHeaders();

            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            services.AddAutoMapper(typeof(MapperInitializer));
            services.AddScoped<IAuthManager, AuthManager>();

            services.AddCors();

            services.AddControllers();
            services.AddSwagger();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(opt =>
            opt.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhoneBookApp v1"));
            }

            app.ConfigureExceptionhandler();

            app.UseHttpsRedirection();

            app.UseHttpCacheHeaders();

            app.UseRouting();
            //app.ConfigureCors(Configuration);


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
