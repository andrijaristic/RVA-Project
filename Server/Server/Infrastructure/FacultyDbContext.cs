using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Infrastructure
{
    public class FacultyDbContext : DbContext
    {
        //Ovde definisemo DbSetove (tabele)
        public DbSet<User> Users { get; set; }

        public FacultyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FacultyDbContext).Assembly);
        }
    }
}
