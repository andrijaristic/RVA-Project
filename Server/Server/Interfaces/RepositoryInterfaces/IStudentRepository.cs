using Server.Models;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student> GetStudentAsync(int studentId);
        Student GetStudent(int studentId);
    }
}
