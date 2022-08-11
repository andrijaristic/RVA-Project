using Server.Interfaces.RepositoryInterfaces;

namespace Server.Interfaces.UnitOfWorkInterfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        Task SaveAsync();
        void Save();
    }
}
