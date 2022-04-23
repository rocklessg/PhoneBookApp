using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PhoneBookApplication.Core.Entities;
using PhoneBookApplication.Infrastructure.Data.DatabaseContexts;

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
    }
}
