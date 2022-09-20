using Server.Dto.StudentDto;
using Server.Models;

namespace Server.Dto.StudentResultDto
{
    public class DisplayStudentResultDTO
    {
        public int Id { get; set; }
        public DisplayStudentDTO Student { get; set; }
        public bool Result { get; set; }
        public bool isTouched { get; set; }
    }
}
