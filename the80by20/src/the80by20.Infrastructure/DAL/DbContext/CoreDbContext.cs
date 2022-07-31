using Microsoft.EntityFrameworkCore;
using the80by20.App.Core.SolutionToProblem.ReadModel;
using the80by20.App.MasterData.CategoryCrud;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Domain.Security.UserEntity;
using the80by20.Infrastructure.Core.SolutionToProblem;
using the80by20.Infrastructure.Core.SolutionToProblem.Adapters;
using the80by20.Infrastructure.MasterData.Adapters;
using the80by20.Infrastructure.Security.Adapters.Users;

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
            
            MasterDataSchema.MapUsing(modelBuilder);
            UserSchema.MapUsing(modelBuilder);
            SolutionToProblemSchema.MapUsing(modelBuilder);
        }
    }
}