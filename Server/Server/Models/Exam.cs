namespace Server.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public DateTime ExamDate { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<StudentResult> StudentResults { get; set; } = new List<StudentResult>();

        public Exam DeepCopy()
        {
            Exam deepCopy = new Exam();
            deepCopy.ExamName = this.ExamName;
            deepCopy.SubjectId = this.SubjectId;
            deepCopy.ExamDate = this.ExamDate;

            if (this.Subject != null)
            {
                deepCopy.Subject = this.Subject.DeepCopy();
            }

            return deepCopy;
        }
    }
}
