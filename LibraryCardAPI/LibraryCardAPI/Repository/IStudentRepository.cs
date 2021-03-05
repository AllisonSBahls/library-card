using LibraryCardAPI.Models;
using LibraryCardAPI.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Repository
{
    public interface IStudentRepository : IRepository
    {
        Task<Student> FindByIdAsync(int id);
        Task<List<Student>> FindWithPagedSearchName(string name, int size, int offset);
        int GetCount(string name);
        void RenewValidateStudent(Student student);
    }
}
