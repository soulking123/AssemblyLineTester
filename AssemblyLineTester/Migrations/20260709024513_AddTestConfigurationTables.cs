using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddTestConfigurationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestConfigurations",
                columns: table => new
                {
                    ConfigId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigCode = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigName = table.Column<string>(type: "TEXT", nullable: false),
                    ProductModel = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestConfigurations", x => x.ConfigId);
                });

            migrationBuilder.CreateTable(
                name: "TestConfigNets",
                columns: table => new
                {
                    NetConfigId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigId = table.Column<int>(type: "INTEGER", nullable: false),
                    SequenceNo = table.Column<int>(type: "INTEGER", nullable: false),
                    HarnessGroup = table.Column<string>(type: "TEXT", nullable: false),
                    FromAddress = table.Column<int>(type: "INTEGER", nullable: false),
                    FromLabel = table.Column<string>(type: "TEXT", nullable: false),
                    ToAddress = table.Column<int>(type: "INTEGER", nullable: false),
                    ToLabel = table.Column<string>(type: "TEXT", nullable: false),
                    TestTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    LowerLimit = table.Column<double>(type: "REAL", nullable: true),
                    UpperLimit = table.Column<double>(type: "REAL", nullable: true),
                    MeasurementUnit = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestConfigNets", x => x.NetConfigId);
                    table.ForeignKey(
                        name: "FK_TestConfigNets_TestConfigurations_ConfigId",
                        column: x => x.ConfigId,
                        principalTable: "TestConfigurations",
                        principalColumn: "ConfigId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestConfigNets_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "MaintenanceStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUsageDate",
                value: new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_TestConfigNets_ConfigId_SequenceNo",
                table: "TestConfigNets",
                columns: new[] { "ConfigId", "SequenceNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestConfigNets_TestTypeId",
                table: "TestConfigNets",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestConfigurations_ConfigCode",
                table: "TestConfigurations",
                column: "ConfigCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestConfigNets");

            migrationBuilder.DropTable(
                name: "TestConfigurations");

            migrationBuilder.UpdateData(
                table: "MaintenanceStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUsageDate",
                value: new DateTime(2026, 7, 8, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
