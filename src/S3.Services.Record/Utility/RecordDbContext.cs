using Microsoft.EntityFrameworkCore;
using S3.Services.Record.Domain;

namespace S3.Services.Record.Utility
{
    public class RecordDbContext : DbContext
    {
        public RecordDbContext(DbContextOptions<RecordDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Rule> Rules { get; set; }
        public DbSet<StudentScore> StudentScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Apply custum configurations using reflextion to scan for configuration files
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);
        }
    }
}
