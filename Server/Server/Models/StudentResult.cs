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
        public bool isTouched { get; set; } = false;

        public StudentResult DeepCopy() 
        {
            StudentResult deepCopy = new StudentResult();
            deepCopy.ExamId = this.ExamId;
            deepCopy.StudentId = this.StudentId;
            deepCopy.Result = this.Result;
            deepCopy.isTouched = this.isTouched;

            return deepCopy;
        }
    }
}
