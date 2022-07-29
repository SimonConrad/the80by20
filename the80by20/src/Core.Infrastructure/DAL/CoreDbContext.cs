using Core.App.Administration.MasterData;
using Core.App.Administration.Users;
using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SolutionToProblem.Operations;
using Core.Infrastructure.DAL.Administration;
using Core.Infrastructure.DAL.SolutionToProblem;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DAL
{
    // in future do some in memory or not in memopry sqllite for testing purposes
    public class CoreDbContext : DbContext
    {
        #region write models
        public DbSet<SolutionToProblemAggregate> SolutionsToProblemsAggregates { get; set; }
        public DbSet<SolutionToProblemCrudData> SolutionsToProblemsCrudData { get; set; }
        #endregion

        #region read models
        public DbSet<SolutionToProblemReadModel> SolutionsToProblemsReadModel { get; set; }
        #endregion

        #region crud models
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // todo move to extensions
            optionsBuilder
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO think if needed
            base.OnModelCreating(modelBuilder);
            
            SolutionToProblemSchema.MapUsing(modelBuilder);
            AdministrationSchema.MapUsing(modelBuilder);
        }
    }
}