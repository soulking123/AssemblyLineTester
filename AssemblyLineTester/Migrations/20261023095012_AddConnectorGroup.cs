using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddConnectorGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConnectorGroup",
                table: "TestDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConnectorGroup",
                table: "TestConfigNets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectorGroup",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "ConnectorGroup",
                table: "TestConfigNets");
        }
    }
}
