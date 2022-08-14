using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;
using System.Linq;

namespace Server.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(FacultyDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<User> FindUserByUsername(string username)
        {
            User user = await _dbContext.Users.SingleOrDefaultAsync<User>(u => string.Equals(u.Username, username));
            return user;
        }

        public List<User> GetAllStudentUsers()
        {
            return _dbContext.Users.Where(u => u.UserType == Enums.EUserType.STUDENT).ToList();
        }
    }
}
