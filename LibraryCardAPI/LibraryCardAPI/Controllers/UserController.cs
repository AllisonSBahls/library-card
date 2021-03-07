using LibraryCardAPI.DTO;
using LibraryCardAPI.Models;
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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _service.FindByIdAsync(id);
                if (result == null) return NotFound("no users found");
                return Ok(result);

            }catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in search the user: {ex.Message}");
            }
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        public async Task<IActionResult> Get([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            try
            {
                var result = await _service.FindWithUserFullNamePageSearch(name, sortDirection, pageSize, page);
                if (result == null) return NotFound("no users found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in search the user: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserDTO userDTO)
        {
            try
            {
                var result = await _service.CreateUserAsync(userDTO);
                if (result == null) return BadRequest("Error create user");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in search the user: {ex.Message}");
            }
        }
        [HttpPut("password/{id}")]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword, int id)
        {
            try
            {
               await _service.ChangeUserPassword(id, changePassword);
               return this.StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in change password of user: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UserDTO userDTO)
        {
            try
            {
                var result = await _service.UpdateUserAsync(id, userDTO);
                if (result == null) return BadRequest("Error in update the user");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in search the user: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _service.DeleteUserAsync(id) ?
                   Ok("Deleted") :
                   BadRequest("Student not delete");
           
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in search the user: {ex.Message}");
            }
        }
    }
}
