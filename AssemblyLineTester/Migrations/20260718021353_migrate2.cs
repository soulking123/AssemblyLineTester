using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class migrate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MaintenanceStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUsageDate",
                value: new DateTime(2026, 7, 18, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MaintenanceStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUsageDate",
                value: new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
