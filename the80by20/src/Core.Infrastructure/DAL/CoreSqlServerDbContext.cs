using System.Data.Common;
using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SolutionToProblem.Operations;
using Core.Infrastructure.DAL.Repositories.SolutionToProblem;
using Microsoft.Data.Sqlite;

//using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Core.Infrastructure.DAL
{
    public class CoreSqlServerDbContext : DbContext
    {
        //private readonly DbConnection _connection;

        #region write models
        public DbSet<SolutionToProblemAggregate> SolutionToProblemAggregate { get; set; }
        public DbSet<SolutionToProblemCrudData> SolutionToProblemCrudData { get; set; }
        #endregion

        #region read models
        public DbSet<SolutionToProblemReadModel> SolutionToProblemReadModel { get; set; }
        #endregion


        //public static DbConnection CreateInMemoryDatabase()
        //{
        //    var connection = new SqliteConnection("Filename=:memory:");
        //    connection.Open();
        //    return connection;
        //}

        //public CoreSqlServerDbContext(DbConnection connection)
        //{
        //    _connection = connection;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.Us

            ////optionsBuilder
            ////    .LogTo(Console.WriteLine)
            ////    .EnableSensitiveDataLogging()
            ////    .EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SolutionToProblemSchema.MapUsing(modelBuilder);
        }
    }
}