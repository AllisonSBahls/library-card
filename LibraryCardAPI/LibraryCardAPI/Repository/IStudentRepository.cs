using LibraryCardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Repository
{
    public interface IStudentRepository
    {
        Task CreateStudentsAsync(Student student);
        Task UpdateStudentsAsync(Student item, Student student);
        Task<Student> FindByIdAsync(int id);
        Task<List<Student>> FindWithPagedSearchName(string name, int size, int offset);
        Task<List<Student>> FindWithPagedSearchValidate(DateTime initialDate, DateTime finalDate, int size, int offset);
        int GetCount(int id, string name, DateTime? validate);
        Task DeleteStudentsAsync(Student student);
    }
}
