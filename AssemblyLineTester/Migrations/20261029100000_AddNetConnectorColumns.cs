using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddNetConnectorColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromConnector",
                table: "TestDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToConnector",
                table: "TestDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FromConnector",
                table: "TestConfigNets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToConnector",
                table: "TestConfigNets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromConnector",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "ToConnector",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "FromConnector",
                table: "TestConfigNets");

            migrationBuilder.DropColumn(
                name: "ToConnector",
                table: "TestConfigNets");
        }
    }
}
