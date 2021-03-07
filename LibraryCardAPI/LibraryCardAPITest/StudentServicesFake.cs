using LibraryCardAPI.DTO;
using LibraryCardAPI.Service;
using LibraryCardAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCardAPITest
{
    class StudentServicesFake : IStudentService
    {
        public Task<StudentDTO> CreateStudentsAsync(StudentDTO studentDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteStudentsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDTO> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PageList<StudentDTO>> FindWithPagedSearchName(string name, string sortDirection, int pageSize, int page)
        {
            throw new NotImplementedException();
        }

        public Task RenewValidateStudent(int id, StudentDTO studentDTO)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDTO> UpdateStudentsAsync(int id, StudentDTO studentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
