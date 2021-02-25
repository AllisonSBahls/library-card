using LibraryCardAPI.Models;
using LibraryCardAPI.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly LibraryCardContext _context;

        public StudentRepository(LibraryCardContext context)
        {
            _context = context;
        }

        public async Task<Student> FindByIdAsync(int id)
        {
            try
            {
                var result = await _context.Students.SingleOrDefaultAsync(p => p.Id.Equals(id));
                return result;
            } catch (Exception e)
            {
                throw new Exception("Error return students: " + e.Message);
            }
        }


        public Task<List<Student>> FindWithPagedSearchName(string name, int size, int offset)
        {
            throw new NotImplementedException();
        }

        public Task<List<Student>> FindWithPagedSearchValidate(DateTime initialDate, DateTime finalDate, int size, int offset)
        {
            throw new NotImplementedException();
        }

        public async Task CreateStudentsAsync(Student student)
        {
            try { 
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error insert students: " + e.Message);
            }
        }

        public async Task UpdateStudentsAsync(Student item, Student student)
        {
            try
            {
                _context.Entry(item).CurrentValues.SetValues(student);]
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error update students: " + e.Message);

            }
        }
        public async Task DeleteStudentsAsync(Student student)
        {
            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error in delete students: " + e.Message);
                ;
            }
        }

        public int GetCount(int id, string name, DateTime? validate)
        {
            throw new NotImplementedException();
        }

    }
}
