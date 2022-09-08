using Server.Enums;
using Server.Models;

namespace Server.Dto.UserDto
{
    public class AuthenticatedDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public EUserType UserType { get; set; }
        //public List<Exam> Exams { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
