using Server.Models;
using Server.Repositories;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Subject GetSubject(int subjectId);
        Task<Subject> GetSubjectAsync(int subjectId);
    }
}
