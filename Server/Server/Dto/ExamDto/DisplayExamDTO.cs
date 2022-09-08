using Server.Dto.StudentResultDto;
using Server.Dto.SubjectDto;

namespace Server.Dto.ExamDto
{
    public class DisplayExamDTO
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public DisplaySubjectDTO Subject { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
