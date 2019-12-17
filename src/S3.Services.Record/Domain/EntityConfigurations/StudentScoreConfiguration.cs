using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S3.Services.Record.Domain;

namespace S3.Services.Record.Domain.EntityConfigurations
{
    internal class StudentScoreConfiguration : IEntityTypeConfiguration<StudentScore>
    {
        public void Configure(EntityTypeBuilder<StudentScore> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.SchoolId).IsRequired();
            builder.Property(x => x.ClassId).IsRequired();
            builder.Property(x => x.StudentId).IsRequired();
            builder.Property(x => x.StudentName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Subject).HasMaxLength(30).IsRequired();
            builder.Property(x => x.ClassName).HasMaxLength(20).IsRequired();
            builder.Property(x => x.ExamType).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Term).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Session).IsRequired();
            builder.Property(x => x.Mark).IsRequired();
        }
    }
}
