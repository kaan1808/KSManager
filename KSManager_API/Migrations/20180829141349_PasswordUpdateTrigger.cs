using Microsoft.EntityFrameworkCore.Migrations;

namespace KSManager_API.Migrations
{
    public partial class PasswordUpdateTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE TRIGGER UpdateLastChanges ON PasswordStorageDatas " +
                                 "AFTER UPDATE " +
                                 "AS " +
                                 "BEGIN " +
                                 "SET NOCOUNT ON;" +
                                 "IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;" +
                                 "DECLARE @Id uniqueidentifier;" +
                                 "SELECT @Id = INSERTED.[Id]" +
                                 "FROM INSERTED " +
                                 "UPDATE [PasswordStorageDatas]" +
                                 "SET [LastChanges] = GETUTCDATE()" +
                                 "WHERE [Id] = @Id " +
                                 "END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER UpdateLastChanges;");
        }
    }
}
