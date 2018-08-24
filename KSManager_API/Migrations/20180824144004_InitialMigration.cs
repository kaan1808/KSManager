using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KSManager_API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 64, nullable: false),
                    Password = table.Column<byte[]>(maxLength: 64, nullable: false),
                    Salt = table.Column<byte[]>(maxLength: 32, nullable: false),
                    Iterations = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 64, nullable: true),
                    LastName = table.Column<string>(maxLength: 64, nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PasswordStorageDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Icon = table.Column<byte[]>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    SecurityQuestion = table.Column<string>(nullable: true),
                    SecurityAnswer = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    LastChanges = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordStorageDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordStorageDatas_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordStorageDatas_UserId",
                table: "PasswordStorageDatas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordStorageDatas");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
