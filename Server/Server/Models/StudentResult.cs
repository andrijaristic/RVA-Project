namespace Server.Models
{
    public class StudentResult
    {
        public int Id { get; set; }
        public int ExamId { get; set; } // CAN'T STORE ENTIRE OBJECT.
        public Exam Exam { get; set; }
        public int StudentId { get; set; } // CAN'T STORE ENTIRE OBJECT.
        public Student Student { get; set; }
        public bool Result { get; set; } = false; // True => Passed | False => Failed

        // Reference ka Ispitu i Studentu u bazi.
        //public int ExamId { get; set; }
        //public int StudentId { get; set; }
    }
}
