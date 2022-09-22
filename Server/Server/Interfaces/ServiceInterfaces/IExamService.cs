using Server.Dto;
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
        Task<SuccessDTO> DeleteExam(int id);
        Task<SuccessDTO> UpdateExam(UpdateExamDTO dto);

    }
}
