using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddDiodeDualValuesToTestDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ExpectedValue2",
                table: "TestDetails",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LowerLimit2",
                table: "TestDetails",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Tolerance2",
                table: "TestDetails",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UpperLimit2",
                table: "TestDetails",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MeasuredValue2",
                table: "TestDetails",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasurementPrefix",
                table: "TestDetails",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasurementPrefix2",
                table: "TestDetails",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedValue2",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "LowerLimit2",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "Tolerance2",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "UpperLimit2",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "MeasuredValue2",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "MeasurementPrefix",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "MeasurementPrefix2",
                table: "TestDetails");
        }
    }
}
