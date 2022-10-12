using Microsoft.EntityFrameworkCore;
using the80by20.Masterdata.App.CategoryCrud;

namespace the80by20.Masterdata.Infrastructure.EF
{
    public class MasterDataDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public MasterDataDbContext(DbContextOptions<MasterDataDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("masterdata");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
