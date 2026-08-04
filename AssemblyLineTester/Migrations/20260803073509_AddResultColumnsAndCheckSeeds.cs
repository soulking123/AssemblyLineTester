using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddResultColumnsAndCheckSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ExpectedValue",
                table: "TestDetails",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromLabel",
                table: "TestDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToLabel",
                table: "TestDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Tolerance",
                table: "TestDetails",
                type: "REAL",
                nullable: true);

            migrationBuilder.InsertData(
                table: "LearnConnections",
                columns: new[] { "Id", "DisplayName", "IsChecked", "TestTypeId" },
                values: new object[,]
                {
                    { 1, "Wire", true, 15 },
                    { 2, "Resistance", true, 11 },
                    { 3, "Capacitor", true, 3 },
                    { 4, "Diode", true, 6 },
                    { 5, "Twisted Pair", true, 14 }
                });

            migrationBuilder.InsertData(
                table: "TestResultChecks",
                columns: new[] { "Id", "IsChecked", "TestResultName" },
                values: new object[,]
                {
                    { 1, true, "Net-ID" },
                    { 2, true, "Harness Group" },
                    { 3, true, "From" },
                    { 4, true, "Net From Label" },
                    { 5, true, "To" },
                    { 6, true, "Net To Label" },
                    { 7, true, "Test Type" },
                    { 8, true, "Lower Limit" },
                    { 9, true, "Upper Limit" },
                    { 10, true, "Expected Value" },
                    { 11, true, "Tolerance" },
                    { 12, true, "Measurement" }
                });

            migrationBuilder.InsertData(
                table: "TestTypeChecks",
                columns: new[] { "Id", "DisplayName", "IsChecked", "TestTypeName" },
                values: new object[,]
                {
                    { 1, "4RESISTOR", true, "4RESISTOR" },
                    { 2, "4WIRE", true, "4WIRE" },
                    { 3, "CAPACITOR", true, "CAPACITOR" },
                    { 4, "CLOSE", true, "CLOSE" },
                    { 5, "COMMENT", true, "COMMENT" },
                    { 6, "DIODE", true, "DIODE" },
                    { 7, "LINK", true, "LINK" },
                    { 8, "LUA COMPONENT", true, "LUA COMPONENT" },
                    { 9, "OPEN", true, "OPEN" },
                    { 10, "PROBE", true, "PROBE" },
                    { 11, "RESISTOR", true, "RESISTOR" },
                    { 12, "SIGNATURE DIODE", true, "SIGNATURE DIODE" },
                    { 13, "SIGNATURE WIRE", true, "SIGNATURE WIRE" },
                    { 14, "TWISTED PAIR", true, "TWISTED PAIR" },
                    { 15, "WIRE", true, "WIRE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LearnConnections",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LearnConnections",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LearnConnections",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LearnConnections",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LearnConnections",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TestResultChecks",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TestTypeChecks",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DropColumn(
                name: "ExpectedValue",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "FromLabel",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "ToLabel",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "Tolerance",
                table: "TestDetails");
        }
    }
}
