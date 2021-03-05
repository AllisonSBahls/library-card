using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  Course { get; set; }
        public int RegistrationNumber { get; set; }
        public string Photo { get; set; }
        public string Validate { get; set; }

    }
}
