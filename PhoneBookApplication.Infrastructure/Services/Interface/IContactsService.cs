using PhoneBookApplication.Core.Entities;
using PhoneBookApplication.Core.Services.InfrastructureServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApplication.Infrastructure.Services.Interface
{
    public interface IContactsService : IPhoneBookQueryCommand<Contact>
    {

    }
}
