using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddDiodeSecondValuesToTestConfigNet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ExpectedValue2",
                table: "TestConfigNets",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LowerLimit2",
                table: "TestConfigNets",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Tolerance2",
                table: "TestConfigNets",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UpperLimit2",
                table: "TestConfigNets",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedValue2",
                table: "TestConfigNets");

            migrationBuilder.DropColumn(
                name: "LowerLimit2",
                table: "TestConfigNets");

            migrationBuilder.DropColumn(
                name: "Tolerance2",
                table: "TestConfigNets");

            migrationBuilder.DropColumn(
                name: "UpperLimit2",
                table: "TestConfigNets");
        }
    }
}
