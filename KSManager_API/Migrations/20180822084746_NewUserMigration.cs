using Microsoft.EntityFrameworkCore.Migrations;

namespace KSManager_API.Migrations
{
    public partial class NewUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "salt",
                table: "User",
                newName: "Salt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "User",
                newName: "salt");
        }
    }
}
