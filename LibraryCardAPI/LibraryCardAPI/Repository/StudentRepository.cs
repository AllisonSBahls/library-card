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
            var result = _context.Students.Where(s => s.Name.Contains(name));

            return await result.OrderBy(n => n.Name).Skip(offset).Take(size).ToListAsync();

        }

        public async Task CreateStudentsAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentsAsync(Student item, Student student)
        {
            _context.Entry(item).CurrentValues.SetValues(student);
                await _context.SaveChangesAsync();

        }
        public async Task DeleteStudentsAsync(Student student)
        {

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public int GetCount(string name)
        {
            return _context.Students.Where(x => x.Name.Contains(name)).Count();
        }

    }
}
