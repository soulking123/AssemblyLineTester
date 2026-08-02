using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class MoveTestTypeValueToTestType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestTypeValue",
                table: "LearnConnections");

            migrationBuilder.AddColumn<int>(
                name: "TestTypeValue",
                table: "TestTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "TestTypeValue",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "TestTypeValue",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "TestTypeValue",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "TestTypeValue",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "TestTypeValue",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "TestTypeValue",
                value: 4);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "TestTypeValue",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "TestTypeValue",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "TestTypeValue",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "TestTypeValue",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 11,
                column: "TestTypeValue",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "TestTypeValue",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 13,
                column: "TestTypeValue",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 14,
                column: "TestTypeValue",
                value: 8);

            migrationBuilder.UpdateData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 15,
                column: "TestTypeValue",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestTypeValue",
                table: "TestTypes");

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
    }
}
