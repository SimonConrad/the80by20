using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace the80by20.Masterdata.Infrastructure.EF.Migrations
{
    public partial class testmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name1",
                schema: "masterdata",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name1",
                schema: "masterdata",
                table: "Categories");
        }
    }
}
