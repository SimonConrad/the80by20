using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace the80by20.Masterdata.Infrastructure.EF.Migrations
{
    public partial class addcolumndescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "masterdata",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "masterdata",
                table: "Categories");
        }
    }
}
