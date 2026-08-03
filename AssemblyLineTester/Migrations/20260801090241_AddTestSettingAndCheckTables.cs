using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddTestSettingAndCheckTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultTestSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalTestPointsAvailable = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultTestTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultMaxWireResistance = table.Column<double>(type: "REAL", nullable: false),
                    DefaultWireInstallationResistance = table.Column<double>(type: "REAL", nullable: false),
                    DefaultResistorValue = table.Column<double>(type: "REAL", nullable: false),
                    DefaultResistorTolerance = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultCapacitorValue = table.Column<double>(type: "REAL", nullable: false),
                    DefaultCapacitorTolerance = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultTestSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefaultTestSettings_TestTypes_DefaultTestTypeId",
                        column: x => x.DefaultTestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LearnConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsChecked = table.Column<bool>(type: "INTEGER", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnConnections_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestResultChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestResultName = table.Column<string>(type: "TEXT", nullable: false),
                    IsChecked = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultChecks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTypeChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    IsChecked = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypeChecks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DefaultTestSettings_DefaultTestTypeId",
                table: "DefaultTestSettings",
                column: "DefaultTestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnConnections_TestTypeId",
                table: "LearnConnections",
                column: "TestTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultTestSettings");

            migrationBuilder.DropTable(
                name: "LearnConnections");

            migrationBuilder.DropTable(
                name: "TestResultChecks");

            migrationBuilder.DropTable(
                name: "TestTypeChecks");
        }
    }
}
