using LibraryCardAPI.Models;
using LibraryCardAPI.Repository.Context;
using LibraryCardAPI.Repository.Generic;
using LibraryCardAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Repository
{
    public class StudentRepository : GenericRepository, IStudentRepository
    {
        public StudentRepository(LibraryCardContext context) : base(context) { }
      

        public async Task<Student> FindByIdAsync(int id)
        {
            var result = await _context.Students.SingleOrDefaultAsync(p => p.Id.Equals(id));
            return result;

        }

        public async Task<List<Student>> FindWithPagedSearchName(string name, int size, int offset)
        {
            var result = _context.Students.Where(s => s.Name.Contains(name)).OrderBy(x => x.Name).Skip(offset).Take(size);
           
            return await result.ToListAsync();

        }

        public int GetCount(string name)
        {
            return _context.Students.Where(x => x.Name.Contains(name)).Count();
        }

        public void RenewValidateStudent(Student student)
        {
            _context.Students.Attach(student);
            _context.Entry(student).Property(x => x.Validate).IsModified = true;
        }
    }
}
