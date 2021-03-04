using LibraryCardAPI.Models;
using LibraryCardAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }


        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        public async Task<IActionResult> Get([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            try
            {
                var result = await _service.FindWithPagedSearchName(name, sortDirection, pageSize, page);
                return Ok(result);
            }catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in search the students: {ex.Message} ");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _service.FindByIdAsync(id);
                if (result == null) return NotFound("No student found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in search the students: {ex.Message} ");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Student student)
        {
            try
            {
                var result = await _service.CreateStudentsAsync(student);
                if (result == null) return BadRequest("Error in create the student");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in search the students: {ex.Message} ");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Student student)
        {
            try
            {
                var result = await _service.UpdateStudentsAsync(id, student);
                if (result == null) return BadRequest("Error in update the student");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in update the students: {ex.Message} ");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _service.DeleteStudentsAsync(id) ? 
                    Ok("Deleted") : 
                    BadRequest("Student not delete");
;
            }catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in delete the student: {ex.Message}");
            }
        }
    }
}
