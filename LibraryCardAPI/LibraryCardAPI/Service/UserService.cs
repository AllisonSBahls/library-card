using AutoMapper;
using AutoMapper.Configuration;
using LibraryCardAPI.DTO;
using LibraryCardAPI.Models;
using LibraryCardAPI.Repository;
using LibraryCardAPI.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Service
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task ChangeUserPassword(UserDTO userDTO, string newPassword)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                IdentityResult result = await _userManager.ChangePasswordAsync(user, user.PasswordHash, newPassword);
             
            }
            catch (Exception ex)
            {
                throw new Exception("Error in create user" + ex.Message);

            }
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                var userToReturn = _mapper.Map<UserDTO>(user);
                if (result.Succeeded) return userToReturn;
            
                return null;
            
            }catch(Exception ex)
            {
                throw new Exception("Error in create user" + ex.Message);
            }
        }



        public async Task<UserDTO> FindByIdAsync(int id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(id);
                if (result == null) return null;

                var userDTO = _mapper.Map<UserDTO>(result);
                return userDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in search the user" + ex.Message);

            }

        }

        public async Task<PageList<UserDTO>> FindWithUserFullNamePageSearch(string name, string sortDirection, int pageSize, int page)
        {
            try
            {
                var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
                var size = (pageSize < 1) ? 10 : pageSize;
                var offset = page > 0 ? (page - 1) * size : 0;
                var user = await _repository.FindByNameAsync(name, size, offset);
                var totalResult = _repository.GetCount(name);
                var userDto = _mapper.Map<List<UserDTO>>(user);

                var searchPage = new PageList<UserDTO>
                {
                    CurrentPage = page,
                    List = userDto,
                    PageSize = size,
                    SortDirections = sort,
                    TotalResults = totalResult
                };
                return searchPage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in create user" + ex.Message);

            }
        }

        public async Task<UserDTO> UpdateUserAsync(int id, UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);

                var result = await _repository.FindByIdAsync(id);
                if (result == null) throw new Exception("User for update not found");

                user.Id = result.Id;

                _repository.Update(result);
                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<UserDTO>( await _repository.FindByIdAsync(user.Id));
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in create user" + ex.Message);

            }
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(id);
                if (result == null) throw new Exception("User for remove not found");

                _repository.Delete(result);

                return await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in create user" + ex.Message);

            }
        }
    }
}
