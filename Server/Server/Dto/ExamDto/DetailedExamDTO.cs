using Server.Dto.StudentResultDto;
using Server.Dto.SubjectDto;
using Server.Models;

namespace Server.Dto.ExamDto
{
    public class DetailedExamDTO
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public DisplaySubjectDTO Subject { get; set; }
        public DateTime ExamDate { get; set; }
        public List<DisplayStudentResultDTO> StudentResults { get; set; }
    }
}
