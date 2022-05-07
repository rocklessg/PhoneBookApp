using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBookApplication.Core.Entities;
using PhoneBookApplication.Core.Models.DTO;

namespace PhoneBookApplication.Infrastructure.Helpers
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Contact, ContactRequestDTO>().ReverseMap();
            CreateMap<Contact, ContactResponseDTO>().ReverseMap();
            CreateMap<AppUser, UserDTO>().ReverseMap();

        }
    }
}
