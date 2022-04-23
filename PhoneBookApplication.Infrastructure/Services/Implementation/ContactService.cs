using PhoneBookApplication.Core.Entities;
using PhoneBookApplication.Infrastructure.Data.DatabaseContexts;
using PhoneBookApplication.Infrastructure.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApplication.Infrastructure.Services.Implementation
{
    public class ContactsService : PhoneBookQueryCommand<Contact>, IContactsService
    {
        public ContactsService(PhoneBookDbContext context) : base(context) { }
    }
}
