using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S3.Services.Record.Domain;

namespace S3.Services.Registration.Domain.EntityConfigurations
{
    internal class StudentScoreConfiguration : IEntityTypeConfiguration<StudentScore>
    {
        public void Configure(EntityTypeBuilder<StudentScore> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.StudentId).IsRequired();
            builder.Property(x => x.Subject).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Class).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Type).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Term).IsRequired();
            builder.Property(x => x.Session).IsRequired();
            builder.Property(x => x.Score).IsRequired();
        }
    }
}
