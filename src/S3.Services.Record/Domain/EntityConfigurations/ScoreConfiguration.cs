//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using S3.Services.Record.Domain;

//namespace S3.Services.Record.Domain.EntityConfigurations
//{
//    internal class ScoreConfiguration : IEntityTypeConfiguration<Score>
//    {
//        public void Configure(EntityTypeBuilder<Score> builder)
//        {
//            builder.Property(x => x.Id).ValueGeneratedOnAdd();
//            builder.Property(x => x.StudentId).IsRequired();
//            builder.Property(x => x.StudentName).HasMaxLength(100).IsRequired();
//            builder.Property(x => x.Mark).IsRequired();
//        }
//    }
//}
