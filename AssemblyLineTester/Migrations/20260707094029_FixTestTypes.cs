using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class FixTestTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "TestTypes",
                columns: new[] { "Id", "DisplayName", "TestTypeName" },
                values: new object[,]
                {
                    { 1, "Continuity Test", "Continuity Test" },
                    { 2, "Short Circuit Test", "Short Circuit Test" },
                    { 3, "Insulation Test", "Insulation Test" },
                    { 4, "Functional Test", "Functional Test" },
                    { 5, "Final Verification", "Final Verification" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TestTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "TestTypes",
                columns: new[] { "Id", "DisplayName", "TestTypeName" },
                values: new object[,]
                {
                    { 1, "", "Capacitance" },
                    { 2, "", "Diodes" },
                    { 3, "", "Resistance" },
                    { 4, "", "Insulation Resistance" },
                    { 5, "", "Dielectric Withstand" }
                });
        }
    }
}
