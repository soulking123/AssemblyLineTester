using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddColorAndGaugeToTestDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "TestDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gauge",
                table: "TestDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "Gauge",
                table: "TestDetails");
        }
    }
}