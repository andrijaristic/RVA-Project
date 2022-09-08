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

        public async Task<Student> GetStudentForUser(string username)
        {
            //Student student = await _dbContext.Students.SingleOrDefaultAsync<Student>(s => s.Username == username);
            //return student;
            throw new Exception();
        }

        public async Task<List<Student>> GetStudentsForUser(string username)
        {
            //List<Student> students = await _dbContext.Students.Where(s => s.Username == username).ToListAsync();
            //return students;
            throw new Exception();
        }
    }
}
