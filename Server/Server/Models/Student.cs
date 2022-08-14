namespace Server.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string LastName { get; set; }
        public List<Exam> Exams { get; set; } = new List<Exam>();
    }
}
