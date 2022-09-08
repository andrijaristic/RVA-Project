namespace Server.Models
{
    public class StudentResult
    {
        public int Id { get; set; }
        public int ExamId { get; set; } 
        public Exam Exam { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public bool Result { get; set; } = false; // True => Passed | False => Failed
    }
}
