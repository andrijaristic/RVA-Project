using Server.Dto.UserDto;
using Server.Models;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Student GetStudent(int studentId);
        Task<Student> GetStudentAsync(int studentId);
        Task<Student> GetStudentForUser(string username);
        Task<List<Student>> GetStudentsForUser(string username);
    }
}
