using AutoMapper;
using Microsoft.Extensions.Configuration;
using LibraryCardAPI.DTO;
using LibraryCardAPI.Models;
using LibraryCardAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;

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
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII
                             .GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                             .GetBytes(_configuration.GetSection("AppSettings:Token").Value))
            };
        }

        public bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
