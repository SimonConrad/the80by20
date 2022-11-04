using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace the80by20.Modules.Users.Infrastructure.EF.Migrations
{
    public partial class usersnewcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Claims",
                schema: "users",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "users",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Claims",
                schema: "users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "users",
                table: "Users");
        }
    }
}
