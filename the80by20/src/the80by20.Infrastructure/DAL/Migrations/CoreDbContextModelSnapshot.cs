﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using the80by20.Infrastructure.DAL.DbContext;

#nullable disable

namespace the80by20.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    partial class CoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("the80by20.App.Administration.MasterData.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("the80by20.App.Administration.Security.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("the80by20.App.Core.SolutionToProblem.ReadModel.SolutionToProblemReadModel", b =>
                {
                    b.Property<Guid>("ProblemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRejected")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RequiredSolutionElementTypes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SolutionAbstract")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SolutionElementTypes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SolutionToProblemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("WorkingOnSolutionEnded")
                        .HasColumnType("bit");

                    b.Property<bool>("WorkingOnSolutionStarted")
                        .HasColumnType("bit");

                    b.HasKey("ProblemId");

                    b.ToTable("SolutionsToProblemsReadModel");
                });

            modelBuilder.Entity("the80by20.Domain.Core.SolutionToProblem.Operations.Problem.ProblemAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Confirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("Rejected")
                        .HasColumnType("bit");

                    b.Property<string>("RequiredSolutionElementTypes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProblemsAggregates");
                });

            modelBuilder.Entity("the80by20.Domain.Core.SolutionToProblem.Operations.Problem.ProblemCrudData", b =>
                {
                    b.Property<Guid>("AggregateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Category")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int");

                    b.HasKey("AggregateId");

                    b.ToTable("ProblemsCrudData");
                });

            modelBuilder.Entity("the80by20.Domain.Core.SolutionToProblem.Operations.Solution.SolutionToProblemAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProblemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RequiredSolutionElementTypes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SolutionAbstract")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SolutionElements")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int");

                    b.Property<bool>("WorkingOnSolutionEnded")
                        .HasColumnType("bit");

                    b.Property<bool>("WorkingOnSolutionStarted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SolutionsToProblemsAggregates");
                });
#pragma warning restore 612, 618
        }
    }
}
