using Server.Dto.ExamDto;

namespace Server.Dto.StudentDto
{
    public class DetailedStudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<DisplayExamDTO> Exams { get; set; }
    }
}
