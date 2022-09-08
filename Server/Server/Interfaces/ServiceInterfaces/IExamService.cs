using Server.Dto.ExamDto;
using Server.Dto.StudentResultDto;
using Server.Models;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface IExamService
    {
        Task<List<DetailedExamDTO>> GetAllExams();
        Task<DetailedExamDTO> GetById(int id);
        Task<DisplayExamDTO> CreateExam(NewExamDTO newExamDTO);
        Task DeleteExam(int id);

    }
}
