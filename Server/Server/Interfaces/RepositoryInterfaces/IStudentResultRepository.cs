using Server.Models;
namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IStudentResultRepository : IGenericRepository<StudentResult>
    {
        public Task<List<StudentResult>> GetExamsForStudent(int studentId);
        public Task<List<StudentResult>> GetStudentsForExam(int id);
        public Task<StudentResult> GetStudentForExam(int studentId, int examId);
    }
}
