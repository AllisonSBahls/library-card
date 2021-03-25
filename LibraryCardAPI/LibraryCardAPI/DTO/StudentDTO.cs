using Microsoft.AspNetCore.Http;
using System;

namespace LibraryCardAPI.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  Course { get; set; }
        public int RegistrationNumber { get; set; }
        public string Photo { get; set; }
        public IFormFile ImageFile { get; set; }
        public DateTime Validate { get; set; }

    }
}
