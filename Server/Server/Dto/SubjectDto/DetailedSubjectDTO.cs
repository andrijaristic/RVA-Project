using Server.Dto.ExamDto;

namespace Server.Dto.SubjectDto
{
    public class DetailedSubjectDTO
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public List<DetailedExamDTO> Exams { get; set; }
    }
}
