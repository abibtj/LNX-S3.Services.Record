using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S3.Services.Record.Domain;

namespace S3.Services.Registration.Domain.EntityConfigurations
{
    internal class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();
            builder.Property(x => x.SchoolId).IsRequired();
        }
    }
}
