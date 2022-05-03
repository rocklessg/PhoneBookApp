using PhoneBookApplication.Core.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApplication.Core.Services.InfrastructureServices
{
    public interface IAuthManager
    {
        Task<string> CreateToken();
        Task<bool> ValidateUser(LoginUserDTO userDTO);
    }
}
