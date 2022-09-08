namespace Server.Interfaces.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> GetAllAsync();
        void Add(T entity);
        List<T> GetAll();
        void Remove(T entity);
    }
}
