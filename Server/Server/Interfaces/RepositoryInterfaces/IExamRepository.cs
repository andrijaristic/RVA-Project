using Server.Models;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IExamRepository : IGenericRepository<Exam>
    {
        Exam GetExam(int examId);
        Task<Exam> GetExamAsync(int examId);
        Task<Exam> GetExamComplete(int examId);
        Task<List<Exam>> GetAllExamsComplete();

    }
}
