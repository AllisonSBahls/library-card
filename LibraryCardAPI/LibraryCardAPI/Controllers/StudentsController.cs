using LibraryCardAPI.DTO;
using LibraryCardAPI.Models;
using LibraryCardAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LibraryCardAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
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
                if (result == null) return NotFound("No students found");
                return Ok(result);
            }
            catch (Exception ex)
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

        [HttpGet("upload/photo")]
        public async Task<IActionResult> UploadPhoto()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                
                if(file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in upload photo the students: {ex.Message} ");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentDTO student)
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
        public async Task<IActionResult> Put(int id, StudentDTO student)
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
        
        [HttpPatch("renew/{id}")]
        public async Task<IActionResult> RenewCard(int id, StudentDTO studentDTO)
        {
            try{
                await _service.RenewValidateStudent(id, studentDTO);
                return this.StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in renew card student: {ex.Message}");
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
