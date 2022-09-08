namespace Server.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public List<Exam> Exams { get; set; } = new List<Exam>();
    }
}
