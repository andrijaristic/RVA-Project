using Server.Dto.StudentResultDto;
using Server.Models;

namespace Server.Dto.ExamDto
{
    public class NewExamDTO
    {
        public string ExamName { get; set; }
        public int SubjectId { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
