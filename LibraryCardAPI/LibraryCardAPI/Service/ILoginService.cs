using LibraryCardAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Service
{
    public interface ILoginService
    {
        Task<LoginDTO> LoginAsync(LoginDTO user);
        bool ValidateToken(string authToken);

    }
}
