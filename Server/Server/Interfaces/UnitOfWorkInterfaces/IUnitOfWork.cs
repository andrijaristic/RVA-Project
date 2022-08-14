using Server.Interfaces.RepositoryInterfaces;

namespace Server.Interfaces.UnitOfWorkInterfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IStudentRepository Students { get; }
        ISubjectRepository Subjects { get; }
        IStudentResultRepository StudentResults { get; }
        IExamRepository Exams { get; }

        Task SaveAsync();
        void Save();
    }
}
