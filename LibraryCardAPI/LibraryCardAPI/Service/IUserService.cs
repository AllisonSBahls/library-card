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
        Task<UserDTO> UpdateUserAsync(int id, UserDTO userDTO);
        Task<UserDTO> CreateUserAsync(UserDTO userDTO);
        Task ChangeUserPassword(UserDTO userDTO, string newPassword);
        Task<bool> DeleteUserAsync(int id);
        Task<PageList<UserDTO>> FindWithUserFullNamePageSearch(string name, string sortDirection, int pageSize, int page);
    }
}
