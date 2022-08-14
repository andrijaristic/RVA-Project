using Server.Models;

namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindUserByUsername(string username);
        List<User> GetAllStudentUsers();
    }
}
