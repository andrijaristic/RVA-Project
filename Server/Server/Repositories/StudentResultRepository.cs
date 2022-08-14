using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces.RepositoryInterfaces;
using Server.Models;

namespace Server.Repositories
{
    public class StudentResultRepository : GenericRepository<StudentResult>, IStudentResultRepository
    {
        public StudentResultRepository(FacultyDbContext dbContext) : base(dbContext)
        {

        }
    }
}
