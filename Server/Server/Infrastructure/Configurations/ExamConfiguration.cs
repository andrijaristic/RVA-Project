using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Models;

namespace Server.Infrastructure.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Subject)
                   .WithMany(x => x.Exams);

            builder.HasMany(x => x.StudentResults)
                   .WithOne(x => x.Exam)
                   .HasForeignKey(x => x.ExamId);

        }
    }
}
