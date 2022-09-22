namespace Server.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string UserUsername { get; set; } 
        public string Name { get; set; }    
        public string LastName { get; set; }
        public List<Exam> Exams { get; set; } = new List<Exam>();

        public Student DeepCopy()
        {
            Student deepCopy = new Student();
            deepCopy.UserUsername = this.UserUsername;
            deepCopy.Name = this.Name;
            deepCopy.LastName = this.LastName;
            
            if (this.Exams != null)
            {
                deepCopy.Exams = this.Exams.ConvertAll(x => x.DeepCopy());
            }

            return deepCopy;
        }
    }
}
