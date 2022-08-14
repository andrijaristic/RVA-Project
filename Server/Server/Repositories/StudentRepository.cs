using Microsoft.EntityFrameworkCore;
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
            Student student = _dbContext.Students.SingleOrDefault<Student>(student => student.Id == studentId);
            return student;
        }

        public async Task<Student> GetStudentAsync(int studentId)
        {
            Student student = await _dbContext.Students.SingleOrDefaultAsync<Student>(student => student.Id == studentId);
            return student;
        }
    }
}
