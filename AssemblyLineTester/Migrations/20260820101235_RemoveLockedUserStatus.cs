using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLockedUserStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Locked accounts fall back to Inactive - same effect (blocked from signing in),
            // but the FK to UserStatuses would otherwise refuse to let the Id=3 row go.
            migrationBuilder.Sql("UPDATE Users SET StatusId = 2 WHERE StatusId = 3;");

            migrationBuilder.DeleteData(
                table: "UserStatuses",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserStatuses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "User is locked out due to multiple failed login attempts", "Locked" });
        }
    }
}
