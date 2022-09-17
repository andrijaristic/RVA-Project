using Server.Dto.ExamDto;
using Server.Dto.StudentDto;

namespace Server.Dto.StudentResultDto
{
    public class StudentExamsDTO
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public DisplayExamDTO Exam { get; set; }
        public int StudentId { get; set; }
        public DisplayStudentDTO Student { get; set; }
    }
}
