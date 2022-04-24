using Microsoft.Extensions.DependencyInjection;
using PhoneBookApplication.Infrastructure.Services.Implementation;
using PhoneBookApplication.Infrastructure.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApplication.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ResolveInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IContactsService, ContactsService>();
        }
    }
}
