using Server.Dto.StudentDto;
using Server.Dto.StudentResultDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface IStudentResultService
    {
        Task<DisplayStudentDTO> AddStudentToExam(AddStudentResultDTO addStudentResultDTO);
        Task<string> RemoveStudentFromExam(AddStudentResultDTO addStudentResultDTO);
        Task<List<DisplayStudentResultDTO>> GetAllStudentsForExam(int id);
        Task<List<StudentExamsDTO>> GetExamsForStudent(int id);
    }
}
