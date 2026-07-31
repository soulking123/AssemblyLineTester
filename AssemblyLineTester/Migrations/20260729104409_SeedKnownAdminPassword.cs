using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class SeedKnownAdminPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$Hcpu0fJL6xvHgmbYJNv/s.QMrlL8F9ALRHip20cUQLY6OSro3YIhC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2b$11$yGJN27FWp/BWcUo0M4ZOW.mWsQGCKQwQfVoemKJyPF2MFLP9ickMS");
        }
    }
}
