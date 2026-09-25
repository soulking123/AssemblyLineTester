using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddImageReportInclusion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IncludedForReport",
                table: "ProductModels",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "IncludedForReport",
                table: "HarnessAssemblies",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "IncludedForReport",
                table: "Connectors",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.UpdateData(
                table: "ProductModels",
                keyColumn: "Id",
                keyValue: 1,
                column: "IncludedForReport",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "ProductModels",
                keyColumn: "Id",
                keyValue: 2,
                column: "IncludedForReport",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "ProductModels",
                keyColumn: "Id",
                keyValue: 3,
                column: "IncludedForReport",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "ProductModels",
                keyColumn: "Id",
                keyValue: 4,
                column: "IncludedForReport",
                value: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncludedForReport",
                table: "ProductModels");

            migrationBuilder.DropColumn(
                name: "IncludedForReport",
                table: "HarnessAssemblies");

            migrationBuilder.DropColumn(
                name: "IncludedForReport",
                table: "Connectors");
        }
    }
}
