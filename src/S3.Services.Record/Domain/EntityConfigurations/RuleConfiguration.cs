using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S3.Services.Record.Domain;

namespace S3.Services.Record.Domain.EntityConfigurations
{
    internal class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.SchoolId).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();
            builder.Property(x => x.CAPercentage).IsRequired();
            builder.Property(x => x.FirstExamPercentage).IsRequired();
            builder.Property(x => x.SecondExamPercentage).IsRequired();
            builder.Property(x => x.HomeworkPercentage).IsRequired();
            builder.Property(x => x.A_DistinctionPoint).IsRequired();
            builder.Property(x => x.B_VeryGoodPoint).IsRequired();
            builder.Property(x => x.C_CreditPoint).IsRequired();
            builder.Property(x => x.P_PassPoint).IsRequired();
        }
    }
}
