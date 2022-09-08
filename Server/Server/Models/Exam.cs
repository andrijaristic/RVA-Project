namespace Server.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public DateTime ExamDate { get; set; }
        public List<StudentResult> StudentResults { get; set; } = new List<StudentResult>();  // List of Students attending exam;
    }
}
