using Microsoft.EntityFrameworkCore;
using Server.Dto.UserDto;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

namespace Server.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(FacultyDbContext dbContext) : base(dbContext)
        {

        }

        public Student GetStudent(int studentId)
        {
            Student student = _dbContext.Students.SingleOrDefault<Student>(s => s.Id == studentId);
            return student;
        }

        public async Task<Student> GetStudentAsync(int studentId)
        {
            Student student = await _dbContext.Students.SingleOrDefaultAsync<Student>(s => s.Id == studentId);
            return student;
        }

        public async Task<Student> GetStudentComplete(int studentId)
        {
            Student student = await _dbContext.Students.Where(x => x.Id == studentId).Include(x => x.Exams).ThenInclude(x => x.Subject).SingleOrDefaultAsync();
            return student;
        }

        public async Task<Student> GetStudentForUser(string username)
        {
            Student student = await _dbContext.Students.FirstOrDefaultAsync<Student>(s => s.UserUsername == username);
            return student;
        }

        public async Task<List<Student>> GetStudentsDetailed()
        {
            List<Student> students = await _dbContext.Students.ToListAsync();
            return students;
        }

        public async Task<List<Student>> GetStudentsForUser(string username)
        {
            List<Student> students = await _dbContext.Students.Where(s => s.UserUsername == username).ToListAsync();
            return students;
        }
    }
}
