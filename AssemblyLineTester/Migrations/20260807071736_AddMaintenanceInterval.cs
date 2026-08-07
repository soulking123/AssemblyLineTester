using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddMaintenanceInterval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntervalDays",
                table: "MaintenanceStates",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "IntervalHours",
                table: "MaintenanceStates",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "MaintenanceStates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IntervalDays", "IntervalHours" },
                values: new object[] { 180, 2160.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervalDays",
                table: "MaintenanceStates");

            migrationBuilder.DropColumn(
                name: "IntervalHours",
                table: "MaintenanceStates");
        }
    }
}
