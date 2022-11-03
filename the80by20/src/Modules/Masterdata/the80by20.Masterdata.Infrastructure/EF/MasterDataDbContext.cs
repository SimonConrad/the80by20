using Microsoft.EntityFrameworkCore;
using the80by20.Modules.Masterdata.App.Entities;

// todo soft delete, using ef mechanism, maybe with: interceptor setting is-deleted whene remove from dbcotxt, interceptor not returns is-delted
// todo audit mechanism, using ef mechanism set inset-timestamp, update-tiemstamp, user-id
namespace the80by20.Modules.Masterdata.Infrastructure.EF
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
            //optionsBuilder
            //    .LogTo(Console.WriteLine);
            //.EnableSensitiveDataLogging()
            //.EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("masterdata");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
