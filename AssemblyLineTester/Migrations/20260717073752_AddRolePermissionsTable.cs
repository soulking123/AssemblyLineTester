using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddRolePermissionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    PermissionKey = table.Column<string>(type: "TEXT", nullable: false),
                    PermissionGroup = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsGranted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "MaintenanceStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUsageDate",
                value: new DateTime(2026, 7, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[,]
                {
                    { 1, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 1 },
                    { 2, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 1 },
                    { 3, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 1 },
                    { 4, "Access Test History Page", true, "Page Access", "Page.TestHistory", 1 },
                    { 5, "Access Reports Page", true, "Page Access", "Page.Reports", 1 },
                    { 6, "Access System Log Page", true, "Page Access", "Page.SystemLog", 1 },
                    { 7, "Access Settings Page", true, "Page Access", "Page.Settings", 1 },
                    { 8, "Access User Management Page", true, "Page Access", "Page.UserManagement", 1 },
                    { 9, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 1 },
                    { 10, "Add and Edit Test Parameters & Harness Specs", true, "Test Operations", "Action.TestConfig.Edit", 1 },
                    { 11, "Calibrate and Configure Hardware Instruments", true, "Test Operations", "Action.Instruments.Calibrate", 1 },
                    { 12, "Add, Edit, and Manage System User Accounts", true, "System & Logs", "Action.UserManagement.Edit", 1 },
                    { 13, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 1 },
                    { 14, "Clear and Purge System Activity Logs", true, "System & Logs", "Action.SystemLog.Clear", 1 },
                    { 15, "Create and Restore Database Backups", true, "System & Logs", "Action.Database.Backup", 1 },
                    { 16, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 2 },
                    { 17, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 2 },
                    { 18, "Access Harness Database Page", false, "Page Access", "Page.HarnessDatabase", 2 },
                    { 19, "Access Test History Page", true, "Page Access", "Page.TestHistory", 2 },
                    { 20, "Access Reports Page", false, "Page Access", "Page.Reports", 2 },
                    { 21, "Access System Log Page", false, "Page Access", "Page.SystemLog", 2 },
                    { 22, "Access Settings Page", false, "Page Access", "Page.Settings", 2 },
                    { 23, "Access User Management Page", false, "Page Access", "Page.UserManagement", 2 },
                    { 24, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 2 },
                    { 25, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 2 },
                    { 26, "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate", 2 },
                    { 27, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 2 },
                    { 28, "Export and Print Test Reports & Analytics", false, "System & Logs", "Action.Reports.Export", 2 },
                    { 29, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 2 },
                    { 30, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 2 },
                    { 31, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 3 },
                    { 32, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 3 },
                    { 33, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 3 },
                    { 34, "Access Test History Page", true, "Page Access", "Page.TestHistory", 3 },
                    { 35, "Access Reports Page", true, "Page Access", "Page.Reports", 3 },
                    { 36, "Access System Log Page", true, "Page Access", "Page.SystemLog", 3 },
                    { 37, "Access Settings Page", true, "Page Access", "Page.Settings", 3 },
                    { 38, "Access User Management Page", false, "Page Access", "Page.UserManagement", 3 },
                    { 39, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 3 },
                    { 40, "Add and Edit Test Parameters & Harness Specs", true, "Test Operations", "Action.TestConfig.Edit", 3 },
                    { 41, "Calibrate and Configure Hardware Instruments", true, "Test Operations", "Action.Instruments.Calibrate", 3 },
                    { 42, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 3 },
                    { 43, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 3 },
                    { 44, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 3 },
                    { 45, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 3 },
                    { 46, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 4 },
                    { 47, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 4 },
                    { 48, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 4 },
                    { 49, "Access Test History Page", true, "Page Access", "Page.TestHistory", 4 },
                    { 50, "Access Reports Page", false, "Page Access", "Page.Reports", 4 },
                    { 51, "Access System Log Page", true, "Page Access", "Page.SystemLog", 4 },
                    { 52, "Access Settings Page", true, "Page Access", "Page.Settings", 4 },
                    { 53, "Access User Management Page", false, "Page Access", "Page.UserManagement", 4 },
                    { 54, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 4 },
                    { 55, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 4 },
                    { 56, "Calibrate and Configure Hardware Instruments", true, "Test Operations", "Action.Instruments.Calibrate", 4 },
                    { 57, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 4 },
                    { 58, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 4 },
                    { 59, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 4 },
                    { 60, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 4 },
                    { 61, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 5 },
                    { 62, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 5 },
                    { 63, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 5 },
                    { 64, "Access Test History Page", true, "Page Access", "Page.TestHistory", 5 },
                    { 65, "Access Reports Page", true, "Page Access", "Page.Reports", 5 },
                    { 66, "Access System Log Page", false, "Page Access", "Page.SystemLog", 5 },
                    { 67, "Access Settings Page", false, "Page Access", "Page.Settings", 5 },
                    { 68, "Access User Management Page", false, "Page Access", "Page.UserManagement", 5 },
                    { 69, "Start and Run Testing Operations", false, "Test Operations", "Action.TestExecution.Run", 5 },
                    { 70, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 5 },
                    { 71, "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate", 5 },
                    { 72, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 5 },
                    { 73, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 5 },
                    { 74, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 5 },
                    { 75, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 5 },
                    { 76, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 6 },
                    { 77, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 6 },
                    { 78, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 6 },
                    { 79, "Access Test History Page", true, "Page Access", "Page.TestHistory", 6 },
                    { 80, "Access Reports Page", true, "Page Access", "Page.Reports", 6 },
                    { 81, "Access System Log Page", true, "Page Access", "Page.SystemLog", 6 },
                    { 82, "Access Settings Page", false, "Page Access", "Page.Settings", 6 },
                    { 83, "Access User Management Page", false, "Page Access", "Page.UserManagement", 6 },
                    { 84, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 6 },
                    { 85, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 6 },
                    { 86, "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate", 6 },
                    { 87, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 6 },
                    { 88, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 6 },
                    { 89, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 6 },
                    { 90, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId_PermissionKey",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionKey" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.UpdateData(
                table: "MaintenanceStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUsageDate",
                value: new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
