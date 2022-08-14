using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

namespace Server.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(FacultyDbContext dbContext) : base(dbContext)
        {

        }

        public Subject GetSubject(int subjectId)
        { 
            Subject subject = _dbContext.Subject.SingleOrDefault<Subject>(subject => subject.Id == subjectId);
            return subject;
        }

        public async Task<Subject> GetSubjectAsync(int subjectId)
        {
            Subject subject = await _dbContext.Subject.SingleOrDefaultAsync<Subject>(subject => subject.Id == subjectId);
            return subject;
        }
    }
}