using Server.Enums;

namespace Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public EUserType UserType { get; set; }
        //public Dictionary<Exam, bool> Exams { get; set; } = new Dictionary<Exam, bool>(); // Dict<Exam, passed/failed>
    }
}
