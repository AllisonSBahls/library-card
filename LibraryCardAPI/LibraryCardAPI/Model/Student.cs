using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String  Course { get; set; }
        public int RegistrationNumber { get; set; }
        public string Photo { get; set; }
        public string Validate { get; set; }
    }
}
