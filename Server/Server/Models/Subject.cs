namespace Server.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public List<User> Students { get; set; } = new List<User>();    // List of Students attending class;
    }
}
