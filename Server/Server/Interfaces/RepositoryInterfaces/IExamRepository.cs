using Server.Models;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IExamRepository : IGenericRepository<Exam>
    {
        Task<Exam> GetExamAsync(int examId);
        Exam GetExam(int examId);
    }
}
