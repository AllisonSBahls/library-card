using LibraryCardAPI.Models;
using LibraryCardAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Service
{
    public interface IStudentService
    {
        Task<Student> CreateStudentsAsync(Student student);
        Task<Student> UpdateStudentsAsync(int id, Student student);
        Task<Student> FindByIdAsync(int id);
        Task<PageList<Student>> FindWithPagedSearchName(string name, string sortDirection, int pageSize, int page);
        Task<bool> DeleteStudentsAsync(int id);
    }
}
