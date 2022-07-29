using Microsoft.EntityFrameworkCore;
using the80by20.App.Administration.MasterData;
using the80by20.App.Administration.Security.User;
using the80by20.App.Core.SolutionToProblem.ReadModel;
using the80by20.Domain.Core.SolutionToProblem.Operations;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Infrastructure.Administration;
using the80by20.Infrastructure.Core.SolutionToProblem;

namespace the80by20.Infrastructure.DAL.DbContext
{
    // in future do some in memory or not in memopry sqllite for testing purposes
    public class CoreDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        #region write models
        public DbSet<SolutionToProblemAggregate> SolutionsToProblemsAggregates { get; set; }
        public DbSet<ProblemAggregate> ProblemsAggregates { get; set; }
        public DbSet<ProblemCrudData> ProblemsCrudData { get; set; }
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