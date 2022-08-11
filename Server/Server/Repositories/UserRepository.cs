using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

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
    }
}
