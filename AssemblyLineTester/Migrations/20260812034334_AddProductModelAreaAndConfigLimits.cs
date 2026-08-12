using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddProductModelAreaAndConfigLimits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HipotVoltage",
                table: "TestConfigurations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "MaxWireResistance",
                table: "TestConfigurations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinComponentResistance",
                table: "TestConfigurations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinInsulationResistance",
                table: "TestConfigurations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Desc = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Desc = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestConfigurationTestType",
                columns: table => new
                {
                    TestConfigurationConfigId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestTypesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestConfigurationTestType", x => new { x.TestConfigurationConfigId, x.TestTypesId });
                    table.ForeignKey(
                        name: "FK_TestConfigurationTestType_TestConfigurations_TestConfigurationConfigId",
                        column: x => x.TestConfigurationConfigId,
                        principalTable: "TestConfigurations",
                        principalColumn: "ConfigId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestConfigurationTestType_TestTypes_TestTypesId",
                        column: x => x.TestTypesId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "Desc", "Name" },
                values: new object[] { 1, null, "Riser Cabinet" });

            migrationBuilder.InsertData(
                table: "ProductModels",
                columns: new[] { "Id", "Desc", "Name" },
                values: new object[,]
                {
                    { 1, null, "6020" },
                    { 2, null, "6030" },
                    { 3, null, "6040" },
                    { 4, null, "6060" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Name",
                table: "Areas",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductModels_Name",
                table: "ProductModels",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestConfigurationTestType_TestTypesId",
                table: "TestConfigurationTestType",
                column: "TestTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "ProductModels");

            migrationBuilder.DropTable(
                name: "TestConfigurationTestType");

            migrationBuilder.DropColumn(
                name: "HipotVoltage",
                table: "TestConfigurations");

            migrationBuilder.DropColumn(
                name: "MaxWireResistance",
                table: "TestConfigurations");

            migrationBuilder.DropColumn(
                name: "MinComponentResistance",
                table: "TestConfigurations");

            migrationBuilder.DropColumn(
                name: "MinInsulationResistance",
                table: "TestConfigurations");
        }
    }
}
