using Microsoft.EntityFrameworkCore;
using the80by20.Modules.Solution.App.ReadModel;
using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Modules.Solution.Domain.Solution.Entities;

// todo do command handler decorator that wrpas into unit of worka transaction - like in my-spot

namespace the80by20.Modules.Solution.Infrastructure.EF
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


        public SolutionDbContext(DbContextOptions<SolutionDbContext> options) : base(options)
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
            // TODO think if needed
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("solutions");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}