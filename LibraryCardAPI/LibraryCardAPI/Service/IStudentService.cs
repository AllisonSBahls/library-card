using LibraryCardAPI.DTO;
using LibraryCardAPI.Utils;
using System.Threading.Tasks;

namespace LibraryCardAPI.Service
{
    public interface IStudentService
    {
        Task<StudentDTO> CreateStudentsAsync(StudentDTO studentDTO);
        Task<StudentDTO> UpdateStudentsAsync(int id, StudentDTO studentDTO);
        Task<StudentDTO> FindByIdAsync(int id);
        Task<PageList<StudentDTO>> FindWithPagedSearchName(string name, string sortDirection, int pageSize, int page, bool generate);
        Task RenewValidateStudent(int id, StudentDTO studentDTO);
        Task GeneratedCard(int id);
        Task<bool> DeleteStudentsAsync(int id);
    }
}
