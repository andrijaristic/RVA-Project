using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;

namespace Server.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FacultyDbContext _dbContext;
        public IUserRepository Users { get; }
        public IStudentRepository Students { get; }
        public IStudentResultRepository StudentResults { get; }
        public ISubjectRepository Subjects { get; }
        public IExamRepository Exams { get; }

        public UnitOfWork(FacultyDbContext dbContext, IUserRepository users, IStudentRepository students, IStudentResultRepository studentResults, ISubjectRepository subjects, IExamRepository exams)
        {
            _dbContext = dbContext;
            Users = users;
            Students = students;
            Subjects = subjects;
            StudentResults = studentResults;
            Exams = exams;
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
