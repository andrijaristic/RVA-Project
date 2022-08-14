using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Models;

namespace Server.Infrastructure.Configurations
{
    public class StudentResultConfiguration : IEntityTypeConfiguration<StudentResult>
    {
        public void Configure(EntityTypeBuilder<StudentResult> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
