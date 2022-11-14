using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace the80by20.Modules.Solution.Infrastructure.EF.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "solutions");

            migrationBuilder.CreateTable(
                name: "ProblemsAggregates",
                schema: "solutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredSolutionTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false),
                    Rejected = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemsAggregates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProblemsCrudData",
                schema: "solutions",
                columns: table => new
                {
                    AggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemsCrudData", x => x.AggregateId);
                });

            migrationBuilder.CreateTable(
                name: "SolutionsToProblemsAggregates",
                schema: "solutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequiredSolutionTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolutionSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolutionElements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddtionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WorkingOnSolutionEnded = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionsToProblemsAggregates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolutionsToProblemsReadModel",
                schema: "solutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredSolutionTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsRejected = table.Column<bool>(type: "bit", nullable: false),
                    SolutionToProblemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SolutionSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolutionElements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingOnSolutionEnded = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionsToProblemsReadModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProblemsAggregates",
                schema: "solutions");

            migrationBuilder.DropTable(
                name: "ProblemsCrudData",
                schema: "solutions");

            migrationBuilder.DropTable(
                name: "SolutionsToProblemsAggregates",
                schema: "solutions");

            migrationBuilder.DropTable(
                name: "SolutionsToProblemsReadModel",
                schema: "solutions");
        }
    }
}
