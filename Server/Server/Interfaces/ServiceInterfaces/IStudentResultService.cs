using Server.Dto.ExamDto;
using Server.Dto.StudentDto;
using Server.Dto.StudentResultDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface IStudentResultService
    {
        Task<DisplayExamDTO> AddStudentToExam(AddStudentResultDTO addStudentResultDTO);
        Task<DisplayExamDTO> RemoveStudentFromExam(AddStudentResultDTO addStudentResultDTO);
        Task<List<DisplayStudentResultDTO>> GetAllStudentsForExam(int id);
        Task<List<StudentExamsDTO>> GetExamsForStudent(int id);
        Task<DisplayStudentResultDTO> GetResultsForStudent(AddStudentResultDTO dto);
        Task<DisplayStudentResultDTO> GradeStudentExam(GradeStudentDTO dto);
    }
}
