using AutoMapper;
using AutoMapper.Configuration;
using LibraryCardAPI.DTO;
using LibraryCardAPI.Models;
using LibraryCardAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryCardAPI.Service
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;


        public LoginService(IMapper mapper, IUserRepository repository, IConfiguration configuration, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LoginDTO> LoginAsync(LoginDTO userDTO)
        {
            var user = await _userManager.FindByNameAsync(userDTO.UserName);
            var result = await _signInManager.CheckPasswordSignInAsync(user, userDTO.Password, false);
            var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userDTO.UserName.ToUpper());
            await _repository.FindByIdAsync(user.Id);

            var userToReturn = _mapper.Map<LoginDTO>(appUser);
            if (result.Succeeded)
            {
                return new LoginDTO
                {
                    Token = GenerateJWToken(appUser).Result,
                    UserName = userToReturn.UserName,
                    FullName = userToReturn.FullName,
                };
            }
            return null;
        }

        private async Task<string> GenerateJWToken(User user)
        {
            var claims = new List<Claims>
            {

            }
        }
  

        public bool ValidateToken(string authToken)
        {
            throw new NotImplementedException();
        }
    }
}
