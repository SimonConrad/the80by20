﻿using System.Data.Common;
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
        #region write models
        public DbSet<SolutionToProblemAggregate> SolutionToProblemAggregate { get; set; }
        public DbSet<SolutionToProblemCrudData> SolutionToProblemCrudData { get; set; }
        #endregion

        #region read models
        public DbSet<SolutionToProblemReadModel> SolutionToProblemReadModel { get; set; }
        #endregion

        public CoreSqlServerDbContext(DbContextOptions<CoreSqlServerDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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