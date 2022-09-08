using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

namespace Server.Repositories
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        public ExamRepository(FacultyDbContext dbContext) : base(dbContext)
        {

        }
        public Exam GetExam(int examId)
        {
            Exam exam = _dbContext.Exams.SingleOrDefault<Exam>(exam => exam.Id == examId);
            return exam;
        }

        public async Task<Exam> GetExamAsync(int examId)
        {
            Exam exam = await _dbContext.Exams.SingleOrDefaultAsync<Exam>(exam => exam.Id == examId);
            return exam;
        }

        public async Task<Exam> GetExamComplete(int examId)
        {
            Exam exam = await _dbContext.Exams.Include(x => x.Subject).Include(x => x.StudentResults).ThenInclude(s => s.Student).SingleOrDefaultAsync<Exam>(x => x.Id == examId);
            return exam;
        }

        public async Task<List<Exam>> GetAllExamsComplete()
        {
            List<Exam> exams = await _dbContext.Exams.Include(x => x.StudentResults).ToListAsync();
            return exams;
        }
    }
}
