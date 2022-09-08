namespace Server.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public List<Exam> Exams { get; set; } = new List<Exam>();
    }
}
