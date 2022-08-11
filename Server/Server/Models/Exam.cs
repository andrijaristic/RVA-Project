namespace Server.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
        public List<User> Students { get; set; } = new List<User>();   // List of Students attending exam; Could possibly make this into a Dict<User, bool>(); -> User, passed/failed.
    }
}
