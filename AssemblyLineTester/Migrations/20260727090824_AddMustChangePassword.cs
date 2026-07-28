using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddMustChangePassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HarnessConnectors",
                keyColumn: "HarnessConnectorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HarnessConnectors",
                keyColumn: "HarnessConnectorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HarnessConnectors",
                keyColumn: "HarnessConnectorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HarnessConnectors",
                keyColumn: "HarnessConnectorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HarnessConnectors",
                keyColumn: "HarnessConnectorId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "HarnessConnectors",
                keyColumn: "HarnessConnectorId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "HarnessConnectors",
                keyColumn: "HarnessConnectorId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Connectors",
                keyColumn: "ConnectorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Connectors",
                keyColumn: "ConnectorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Connectors",
                keyColumn: "ConnectorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Connectors",
                keyColumn: "ConnectorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Connectors",
                keyColumn: "ConnectorId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Connectors",
                keyColumn: "ConnectorId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Connectors",
                keyColumn: "ConnectorId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductHarnesses",
                keyColumn: "ProductHarnessId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductHarnesses",
                keyColumn: "ProductHarnessId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductHarnesses",
                keyColumn: "ProductHarnessId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductHarnesses",
                keyColumn: "ProductHarnessId",
                keyValue: 4);

            migrationBuilder.AddColumn<bool>(
                name: "MustChangePassword",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "MustChangePassword",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MustChangePassword",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Connectors",
                columns: new[] { "ConnectorId", "ContactQuantity", "ContactType", "CurrentRating", "ImagePath", "MountingStyle", "PartNumber", "Perf", "WireSize" },
                values: new object[,]
                {
                    { 1, 64, "Female", 5.0, null, "Panel Mount", "D507F1", "Standard", "18 AWG" },
                    { 2, 64, "Male", 5.0, null, "Panel Mount", "D507F2", "Standard", "18 AWG" },
                    { 3, 32, "Female", 3.0, null, "Free Hanging", "D507F1", "Standard", "20 AWG" },
                    { 4, 64, "Female", 5.0, null, "Panel Mount", "D507F1", "Standard", "18 AWG" },
                    { 5, 64, "Male", 5.0, null, "Panel Mount", "D507F2", "Standard", "18 AWG" },
                    { 6, 16, "Female", 2.0, null, "Panel Mount", "D507F3", "Standard", "22 AWG" },
                    { 7, 64, "Female", 5.0, null, "Panel Mount", "D507F1", "Standard", "18 AWG" }
                });

            migrationBuilder.InsertData(
                table: "ProductHarnesses",
                columns: new[] { "ProductHarnessId", "Area", "ConnectorListName", "ImagePaths", "LastUpdate", "Name", "ProductModel", "ProductType" },
                values: new object[,]
                {
                    { 1, "Engine Control", "[\"J1\",\"J2\"]", "[\"Assets/img/Harness/6040.png\"]", new DateTime(2026, 6, 1, 15, 0, 0, 0, DateTimeKind.Local), "Main Engine Harness 6020", "6020", "AC" },
                    { 2, "Cabin Interior", "[\"J1\"]", "[]", new DateTime(2026, 6, 1, 15, 0, 0, 0, DateTimeKind.Local), "Cabin Interior Harness 6030", "6030", "AC" },
                    { 3, "Chassis", "[\"J1\",\"J2\",\"J3\"]", "[\"Assets/img/Harness/6040.png\"]", new DateTime(2026, 6, 1, 15, 0, 0, 0, DateTimeKind.Local), "Chassis Harness 6040", "6040", "AC" },
                    { 4, "Dashboard", "[\"J1\"]", "[]", new DateTime(2026, 6, 1, 15, 0, 0, 0, DateTimeKind.Local), "Dashboard Harness 6060", "6060", "AC" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CompanyId", "CreatedAt", "Department", "FullName", "LastLogin", "PasswordHash", "RoleId", "StatusId", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 2, "CAT0001", new DateTime(2026, 3, 10, 9, 15, 22, 0, DateTimeKind.Unspecified), "Engineering", "Faiz", new DateTime(2026, 3, 15, 10, 28, 41, 0, DateTimeKind.Unspecified), "$2b$11$VAO3CvR8UbFBe9Pn2sNrG.1Wg5vA7FpwVqPyxQeME/5PGTQ4PYk2u", 1, 1, new DateTime(2026, 3, 15, 10, 28, 41, 0, DateTimeKind.Unspecified), "faiz" },
                    { 3, "CAT0002", new DateTime(2026, 3, 11, 8, 23, 15, 0, DateTimeKind.Unspecified), "Engineering", "Khen", new DateTime(2026, 3, 15, 9, 41, 12, 0, DateTimeKind.Unspecified), "$2b$11$JzVT2cjdgorN0AVlQHKv5uNCt1fNbQvlW7J1nEqd0pk2hDCAihDPe", 3, 1, new DateTime(2026, 3, 15, 9, 41, 12, 0, DateTimeKind.Unspecified), "khen" },
                    { 4, "CAT0003", new DateTime(2026, 3, 12, 14, 10, 5, 0, DateTimeKind.Unspecified), "Production", "Tegar", new DateTime(2026, 3, 15, 9, 55, 33, 0, DateTimeKind.Unspecified), "$2b$11$SsUZzVTYh87OPBYbFMYNO.JNJ44jLe3wBxLq2cg7WFPWl.yHAIi6O", 2, 1, new DateTime(2026, 3, 15, 9, 55, 33, 0, DateTimeKind.Unspecified), "tegar" }
                });

            migrationBuilder.InsertData(
                table: "HarnessConnectors",
                columns: new[] { "HarnessConnectorId", "ConnectorId", "Position", "ProductHarnessId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 1 },
                    { 3, 3, 1, 2 },
                    { 4, 4, 1, 3 },
                    { 5, 5, 2, 3 },
                    { 6, 6, 3, 3 },
                    { 7, 7, 1, 4 }
                });
        }
    }
}
