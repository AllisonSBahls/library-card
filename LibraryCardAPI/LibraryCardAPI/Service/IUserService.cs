using LibraryCardAPI.DTO;
using LibraryCardAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Service
{
    public interface IUserService
    {
        Task<UserDTO> FindByIdAsync(int id);
        Task<UserDTO> UpdateAsync(UserDTO user);
        Task<UserDTO> CreateAsync(UserDTO user);
        Task<LoginDTO> LoginAsync(LoginDTO user);
        Task<UserDTO> ChangePassword(int id, UserDTO user);
        Task DeleteAsync(int id);
        Task<PageList<UserDTO>> FindWithPageSearch(string name, string sortDirection, int pageSize, int page);
        bool ValidateToken(string authToken);
    }
}
