using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemLogAndTestTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MaintenanceStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUsageDate",
                value: new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MaintenanceStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUsageDate",
                value: new DateTime(2026, 7, 8, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
