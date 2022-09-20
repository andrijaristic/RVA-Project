using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

namespace Server.Repositories
{
    public class StudentResultRepository : GenericRepository<StudentResult>, IStudentResultRepository
    {
        public StudentResultRepository(FacultyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<StudentResult>> GetExamsForStudent(int studentId)
        {
            List<StudentResult> examIds = await _dbContext.StudentResult.Where(x => x.StudentId == studentId).Include(x => x.Exam).ThenInclude(x => x.Subject).Include(x => x.Student).ToListAsync();
            return examIds;
        }

        public async Task<List<StudentResult>> GetStudentsForExam(int id)
        {
            List<StudentResult> students = await _dbContext.StudentResult.Where(x => x.ExamId == id).Include(x => x.Student).ToListAsync();
            return students;
        }

        public async Task<StudentResult> GetStudentForExam(int studentId, int examId)
        {
            StudentResult student = await _dbContext.StudentResult.Include(x => x.Student).FirstOrDefaultAsync(x => x.StudentId == studentId && x.ExamId == examId);
            return student;
        }
    }
}
