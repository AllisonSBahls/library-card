using AutoMapper;
using LibraryCardAPI.DTO;
using LibraryCardAPI.Models;
using LibraryCardAPI.Repository;
using LibraryCardAPI.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryCardAPI.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;

        public StudentService()
        {

        }
        public StudentService(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StudentDTO> FindByIdAsync(int id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(id);
                if (result == null) return null;
                return _mapper.Map<StudentDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<PageList<StudentDTO>> FindWithPagedSearchName(string name, string sortDirection, int pageSize, int page, bool generate)
        {
            try
            {
                var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
                var size = (pageSize < 1) ? 10 : pageSize;
                var offset = page > 0 ? (page - 1) * size : 0;

                if (string.IsNullOrEmpty(name))
                {
                    name = "";
                }

                var students = await _repository.FindWithPagedSearchName(name, size, offset, generated);

                var totalResult = _repository.GetCount(name);

                var searchPage = new PageList<StudentDTO>
                {
                    CurrentPage = page,
                    List = _mapper.Map<List<StudentDTO>>(students),
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


        public async Task<StudentDTO> CreateStudentsAsync(StudentDTO studentDTO)
        {
            try
            {
                Student student = _mapper.Map<Student>(studentDTO);
                _repository.Add(student);

                return await _repository.SaveChangesAsync() ?
                       _mapper.Map<StudentDTO>(await _repository.FindByIdAsync(student.Id))
                       :
                       null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in create students" + ex.Message);
            }
        }

        public async Task<StudentDTO> UpdateStudentsAsync(int id, StudentDTO studentDTO)
        {
            try
            {
               var student = _mapper.Map<Student>(studentDTO);
               var result = await _repository.FindByIdAsync(id);
                if (result == null) throw new Exception("Students for update not found");

                student.Id = result.Id;

                _repository.Update(result, student);
                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<StudentDTO>(await _repository.FindByIdAsync(student.Id));
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

        public async Task RenewValidateStudent(int id, StudentDTO studentDTO)
        {
            try
            {
                var renewValidade = new Student() { Id = id, Validate = studentDTO.Validate };
                _repository.RenewValidateStudent(renewValidade);
                await _repository.SaveChangesAsync();
              
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task GeneratedCard(int id)
        {
            try
            {
                var generad = new Student() { Id = id, GeneratedCard = true };
                _repository.GeneratedCard(generad);
                await _repository.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
