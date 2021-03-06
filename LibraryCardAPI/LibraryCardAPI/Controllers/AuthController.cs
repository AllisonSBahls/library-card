using LibraryCardAPI.DTO;
using LibraryCardAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryCardAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _service;

        public AuthController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Auth(LoginDTO userDTO)
        {
            try
            {
                var user = await _service.LoginAsync(userDTO);
                if (user != null) return Created("User", user);
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"error {ex.Message}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromHeader] string token)
        {
            try
            {
                bool result = _service.ValidateToken(token);

                if (result)
                {
                    return this.StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, result);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status405MethodNotAllowed);
            }

        }
    }
}
