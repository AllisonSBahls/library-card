using LibraryCardAPI.DTO;
using LibraryCardAPI.Models;
using LibraryCardAPI.Repository;
using LibraryCardAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Student> FindByIdAsync(int id)
        {
            try
            {

                var result = await _repository.FindByIdAsync(id);
                if (result == null) return null;
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<PageList<Student>> FindWithPagedSearchName(string name, string sortDirection, int pageSize, int page)
        {
            try
            {
                var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
                var size = (pageSize < 1) ? 10 : pageSize;
                var offset = page > 0 ? (page - 1) * size : 0;

                if (string.IsNullOrEmpty(name))
                {
                    name = " ";
                }

                var students = await _repository.FindWithPagedSearchName(name, size, offset);

                var totalResult = _repository.GetCount(name);

                var searchPage = new PageList<Student>
                {
                    CurrentPage = page,
                    List = students,
                    PageSize = size,
                    SortDirections = sort,
                    TotalResults = totalResult
                };
                return searchPage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in create students" + ex.Message);

            }
        }


        public async Task<Student> CreateStudentsAsync(Student student)
        {
            try
            {
                _repository.Add(student);
                return await _repository.SaveChangesAsync() ?
                       await _repository.FindByIdAsync(student.Id) :
                       null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in create students" + ex.Message);
            }
        }

        public async Task<Student> UpdateStudentsAsync(int id, Student student)
        {
            try
            {
                var result = await _repository.FindByIdAsync(id);
                if (result == null) throw new Exception("Students for update not found");

                student.Id = result.Id;

                _repository.Update(result);
                if (await _repository.SaveChangesAsync())
                {
                    return await _repository.FindByIdAsync(student.Id);
                }

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteStudentsAsync(int id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(id);
                if (result == null) throw new Exception("Students for remove not found");

                _repository.Delete<Student>(result);
                return await _repository.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
