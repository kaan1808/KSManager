using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KSManager_API.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastChanges",
                table: "PasswordStorageDatas",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastChanges",
                table: "PasswordStorageDatas",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETUTCDATE()");
        }
    }
}
