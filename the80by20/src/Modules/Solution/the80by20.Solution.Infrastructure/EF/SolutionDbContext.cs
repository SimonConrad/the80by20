using Microsoft.EntityFrameworkCore;
using the80by20.Solution.App.SolutionToProblem.ReadModel;
using the80by20.Solution.Domain.Security.UserEntity;
using the80by20.Solution.Domain.SolutionToProblem.Operations.Problem;
using the80by20.Solution.Domain.SolutionToProblem.Operations.Solution;
using the80by20.Solution.Infrastructure.EF.Configurations;
using the80by20.Solution.Infrastructure.Security.Adapters.Users;

// todo do command handler decorator that wrpas into unit of worka transaction - like in my-spot

namespace the80by20.Solution.Infrastructure.EF
{
    // in future do some in memory or not in memopry sqllite for testing purposes
    public class SolutionDbContext : DbContext
    {
        #region write models
        public DbSet<SolutionToProblemAggregate> SolutionsToProblemsAggregates { get; set; }
        public DbSet<ProblemAggregate> ProblemsAggregates { get; set; }
        public DbSet<ProblemCrudData> ProblemsCrudData { get; set; }
        #endregion

        #region read models
        public DbSet<SolutionToProblemReadModel> SolutionsToProblemsReadModel { get; set; }
        #endregion


        public DbSet<User> Users { get; set; }


        public SolutionDbContext(DbContextOptions<SolutionDbContext> options) : base(options)
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
            // TODO think if needed
            base.OnModelCreating(modelBuilder);


            UserSchema.MapUsing(modelBuilder);
            SolutionToProblemConfiguration.MapUsing(modelBuilder);
        }
    }
}