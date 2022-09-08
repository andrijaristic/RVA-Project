using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;

namespace Server.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly FacultyDbContext _dbContext;

        public GenericRepository(FacultyDbContext dbContext)
        {
            _dbContext = dbContext;
        }   

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);    // Set the entity type to T.
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public List<T> GetAll()
        {
            List<T> entities = _dbContext.Set<T>().ToList();
            return entities;
        }

        public async Task<List<T>> GetAllAsync()
        {
            List<T> entities = await _dbContext.Set<T>().ToListAsync();
            return entities;
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
