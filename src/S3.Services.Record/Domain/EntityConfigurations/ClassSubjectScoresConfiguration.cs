using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S3.Services.Record.Domain;

namespace S3.Services.Record.Domain.EntityConfigurations
{
    internal class ClassSubjectScoresConfiguration : IEntityTypeConfiguration<ClassSubjectScores>
    {
        public void Configure(EntityTypeBuilder<ClassSubjectScores> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.SchoolId).IsRequired();
            builder.Property(x => x.ClassId).IsRequired();
            builder.Property(x => x.ClassName).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Subject).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ExamType).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Term).IsRequired();
            builder.Property(x => x.Session).IsRequired();

            // Relationships
            builder.HasMany(x => x.Scores).WithOne(y => y.ClassSubjectScores).OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
