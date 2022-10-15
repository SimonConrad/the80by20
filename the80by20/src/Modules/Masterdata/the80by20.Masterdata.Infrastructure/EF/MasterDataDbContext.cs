using Microsoft.EntityFrameworkCore;
using the80by20.Masterdata.App.CategoryCrud;
using the80by20.Masterdata.Infrastructure.EF.Configurations;

namespace the80by20.Masterdata.Infrastructure.EF
{
    public class MasterDataDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public MasterDataDbContext(DbContextOptions<MasterDataDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // todo move to extensions
            optionsBuilder
                .LogTo(Console.WriteLine);
            //.EnableSensitiveDataLogging()
            //.EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("masterdata");
            CategoryConfiguration.MapUsing(modelBuilder);
        }
    }
}
