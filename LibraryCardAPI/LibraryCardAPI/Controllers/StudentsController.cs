using LibraryCardAPI.DTO;
using LibraryCardAPI.Models;
using LibraryCardAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    [AllowAnonymous]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly IWebHostEnvironment _hostEnvironment;

        public StudentsController(IStudentService service, IWebHostEnvironment hostEnvironment)
        {
            _service = service;
            _hostEnvironment = hostEnvironment;
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

        [HttpPost]
        public async Task<IActionResult> Post(StudentDTO student)
        {

            try
            {
                Console.Write(student.Photo);
                //student.Photo = await SaveImage(student.ImageFile);
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
                if(student.ImageFile != null)
                {
                    DeleteImage(student.Photo);
                    student.Photo = await SaveImage(student.ImageFile);
                }
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
                var student = await _service.FindByIdAsync(id);
                if(student == null)
                {
                    return NotFound("Student not find");
                }

                DeleteImage(student.Photo);
                return await _service.DeleteStudentsAsync(id) ? 
                    Ok("Deleted") : 
                    BadRequest("Student not delete");

            }catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in delete the student: {ex.Message}");
            }
        }

        [NonAction]
        public async Task<string> SaveImage(FormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Resources", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Resources/Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
