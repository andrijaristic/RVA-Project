using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;

namespace Server.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FacultyDbContext _dbContext;
        public IUserRepository Users { get; }

        public UnitOfWork(FacultyDbContext dbContext, IUserRepository users)
        {
            _dbContext = dbContext;
            Users = users;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
