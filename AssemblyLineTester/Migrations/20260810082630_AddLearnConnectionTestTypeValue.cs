using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddLearnConnectionTestTypeValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestTypeValue",
                table: "LearnConnections",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "LearnConnections",
                keyColumn: "Id",
                keyValue: 1,
                column: "TestTypeValue",
                value: 0);

            migrationBuilder.UpdateData(
                table: "LearnConnections",
                keyColumn: "Id",
                keyValue: 2,
                column: "TestTypeValue",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LearnConnections",
                keyColumn: "Id",
                keyValue: 3,
                column: "TestTypeValue",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LearnConnections",
                keyColumn: "Id",
                keyValue: 4,
                column: "TestTypeValue",
                value: 4);

            migrationBuilder.UpdateData(
                table: "LearnConnections",
                keyColumn: "Id",
                keyValue: 5,
                column: "TestTypeValue",
                value: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestTypeValue",
                table: "LearnConnections");
        }
    }
}
