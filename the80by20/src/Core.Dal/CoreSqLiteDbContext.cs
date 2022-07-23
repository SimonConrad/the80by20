using System.Data.Common;
using Core.Dal.SolutionToProblem;
using Core.Domain.SolutionToProblem;
using Core.Domain.SolutionToProblem.Operations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Core.Dal
{
    // TODO Create SqlServerDbcontext with normal sql server db and migrations
    public class CoreSqLiteDbContext : DbContext
    {
        private readonly DbConnection _connection;
        public DbSet<SolutionToProblemAggregate> SolutionToProblemAggregates { get; set; }
        public DbSet<SolutionToProblemData> SolutionToProblemDatas { get; set; }

        public static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }

        public CoreSqLiteDbContext(DbConnection connection)
        {
            _connection = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlite(_connection);

            optionsBuilder
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SolutionToProblemSchema.MapUsing(modelBuilder);
        }
    }
}