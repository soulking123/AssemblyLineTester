using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class SchemaCleanupIndexesAndUtcTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ---- Data first, while the columns it reads still exist ----

            // ConfigId was never wired up as the foreign key, so EF invented the shadow column
            // ConfigurationConfigId to hold the relationship and left ConfigId at zero. Carry the
            // parent across before the shadow column goes, or every saved test program loses its nets.
            migrationBuilder.Sql(
                "UPDATE TestConfigNets SET ConfigId = ConfigurationConfigId WHERE ConfigurationConfigId IS NOT NULL;");

            // Written to survive either starting point: a database that still holds its parent in
            // the shadow column, and one where ConfigId is already correct and the shadow column is
            // empty. Only a row with no parent in either place is unreachable and cannot take a
            // real foreign key.
            migrationBuilder.Sql(
                "DELETE FROM TestConfigNets WHERE ConfigId IS NULL OR ConfigId = 0;");

            // Every instant in the database is UTC from here on. These three columns were the
            // exceptions - written with DateTime.Now while everything around them used UtcNow - so
            // the existing rows are shifted by this machine's offset to match. SQLite's 'utc'
            // modifier reads the stored value as local time, which is exactly what it was.
            migrationBuilder.Sql(
                "UPDATE Users SET LastLogin = datetime(LastLogin, 'utc') WHERE LastLogin IS NOT NULL;");
            migrationBuilder.Sql(
                "UPDATE TestReports SET TestDateTime = datetime(TestDateTime, 'utc');");
            migrationBuilder.Sql(
                "UPDATE TestReports SET EndDateTime = datetime(EndDateTime, 'utc') WHERE EndDateTime IS NOT NULL;");

            // ---- Schema ----

            migrationBuilder.DropForeignKey(
                name: "FK_TestConfigNets_TestConfigurations_ConfigurationConfigId",
                table: "TestConfigNets");

            migrationBuilder.DropIndex(
                name: "IX_TestConfigNets_ConfigurationConfigId",
                table: "TestConfigNets");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "TestReports");

            migrationBuilder.DropColumn(
                name: "TesterModel",
                table: "TestReports");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "TestDetails");

            migrationBuilder.DropColumn(
                name: "ConfigCode",
                table: "TestConfigurations");

            migrationBuilder.DropColumn(
                name: "ConfigurationConfigId",
                table: "TestConfigNets");

            migrationBuilder.DropColumn(
                name: "ConnectorListName",
                table: "ProductHarnesses");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Amend and Delete Saved Test Reports", "System & Logs", "Action.Reports.Edit", 1 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Delete Test History Records", "System & Logs", "Action.TestHistory.Delete", 1 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Dashboard Page", true, "Page.Dashboard" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Test Execution Page", "Page.TestExecution" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", "Page.HarnessDatabase" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Test History Page", true, "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Reports Page", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access System Log Page", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Settings Page", false, "Page Access", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access User Management Page", "Page Access", "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", true, "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Amend and Delete Saved Test Reports", false, "System & Logs", "Action.Reports.Edit", 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Delete Test History Records", false, "System & Logs", "Action.TestHistory.Delete", 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Dashboard Page", "Page.Dashboard" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Test Execution Page", "Page.TestExecution" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", "Page.HarnessDatabase" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Test History Page", true, "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Reports Page", "Page Access", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access System Log Page", "Page Access", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page Access", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access User Management Page", "Page Access", "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", true, "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", true, "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Export and Print Test Reports & Analytics", "System & Logs", "Action.Reports.Export", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Amend and Delete Saved Test Reports", true, "System & Logs", "Action.Reports.Edit", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Delete Test History Records", "System & Logs", "Action.TestHistory.Delete", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Dashboard Page", "Page.Dashboard" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Test Execution Page", true, "Page.TestExecution" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", "Page Access", "Page.HarnessDatabase" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Test History Page", true, "Page Access", "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Reports Page", false, "Page Access", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access System Log Page", true, "Page Access", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page Access", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access User Management Page", "Page Access", "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", "Test Operations", "Action.Instruments.Calibrate", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Export and Print Test Reports & Analytics", "System & Logs", "Action.Reports.Export", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Create and Restore Database Backups", "System & Logs", "Action.Database.Backup", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Amend and Delete Saved Test Reports", "System & Logs", "Action.Reports.Edit", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Delete Test History Records", "System & Logs", "Action.TestHistory.Delete", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Dashboard Page", true, "Page Access", "Page.Dashboard" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Test Execution Page", true, "Page Access", "Page.TestExecution" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Test History Page", true, "Page Access", "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Reports Page", "Page Access", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access System Log Page", "Page Access", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page Access", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "Description", "IsGranted", "PermissionKey", "RoleId" },
                values: new object[] { "Access User Management Page", false, "Page.UserManagement", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Start and Run Testing Operations", false, "Test Operations", "Action.TestExecution.Run", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Export and Print Test Reports & Analytics", "System & Logs", "Action.Reports.Export", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Clear and Purge System Activity Logs", "System & Logs", "Action.SystemLog.Clear", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Create and Restore Database Backups", "System & Logs", "Action.Database.Backup", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Amend and Delete Saved Test Reports", false, "System & Logs", "Action.Reports.Edit", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Delete Test History Records", "System & Logs", "Action.TestHistory.Delete", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Dashboard Page", true, "Page Access", "Page.Dashboard" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Test Execution Page", true, "Page Access", "Page.TestExecution" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", "Page Access", "Page.HarnessDatabase" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Test History Page", true, "Page Access", "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Reports Page", true, "Page Access", "Page.Reports" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[,]
                {
                    { 91, "Access System Log Page", true, "Page Access", "Page.SystemLog", 6 },
                    { 92, "Access Settings Page", false, "Page Access", "Page.Settings", 6 },
                    { 93, "Access User Management Page", false, "Page Access", "Page.UserManagement", 6 },
                    { 94, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 6 },
                    { 95, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 6 },
                    { 96, "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate", 6 },
                    { 97, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 6 },
                    { 98, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 6 },
                    { 99, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 6 },
                    { 100, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 6 },
                    { 101, "Amend and Delete Saved Test Reports", true, "System & Logs", "Action.Reports.Edit", 6 },
                    { 102, "Delete Test History Records", true, "System & Logs", "Action.TestHistory.Delete", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId",
                unique: true,
                filter: "\"CompanyId\" <> ''");

            migrationBuilder.CreateIndex(
                name: "IX_TestConfigNets_ConfigId",
                table: "TestConfigNets",
                column: "ConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemLogs_Timestamp",
                table: "SystemLogs",
                column: "Timestamp");

            migrationBuilder.AddForeignKey(
                name: "FK_TestConfigNets_TestConfigurations_ConfigId",
                table: "TestConfigNets",
                column: "ConfigId",
                principalTable: "TestConfigurations",
                principalColumn: "ConfigId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestConfigNets_TestConfigurations_ConfigId",
                table: "TestConfigNets");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TestConfigNets_ConfigId",
                table: "TestConfigNets");

            migrationBuilder.DropIndex(
                name: "IX_SystemLogs_Timestamp",
                table: "SystemLogs");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "TestReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TesterModel",
                table: "TestReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "TestDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConfigCode",
                table: "TestConfigurations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConfigurationConfigId",
                table: "TestConfigNets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnectorListName",
                table: "ProductHarnesses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Dashboard Page", "Page Access", "Page.Dashboard", 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test Execution Page", "Page Access", "Page.TestExecution", 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", false, "Page.HarnessDatabase" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Test History Page", "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Reports Page", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access System Log Page", false, "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access User Management Page", "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", false, "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", "System & Logs", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", "System & Logs", "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", "Action.Database.Backup" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test History Page", true, "Page Access", "Page.TestHistory", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Reports Page", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access System Log Page", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access User Management Page", false, "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", "System & Logs", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", "System & Logs", "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test Execution Page", "Page Access", "Page.TestExecution", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test History Page", true, "Page Access", "Page.TestHistory", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Reports Page", false, "Page Access", "Page.Reports", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access System Log Page", "Page Access", "Page.SystemLog", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access User Management Page", false, "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", true, "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", "System & Logs", "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", "System & Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test Execution Page", "Page Access", "Page.TestExecution", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test History Page", "Page Access", "Page.TestHistory", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Reports Page", true, "Page Access", "Page.Reports", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access System Log Page", "Page Access", "Page.SystemLog", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Settings Page", "Page Access", "Page.Settings", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access User Management Page", "Page Access", "Page.UserManagement", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", false, "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", "System & Logs", "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", "System & Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", "System & Logs", "Action.Database.Backup" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "Description", "IsGranted", "PermissionKey", "RoleId" },
                values: new object[] { "Access Dashboard Page", true, "Page.Dashboard", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test History Page", true, "Page Access", "Page.TestHistory", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Reports Page", true, "Page Access", "Page.Reports", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access System Log Page", "Page Access", "Page.SystemLog", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Settings Page", "Page Access", "Page.Settings", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access User Management Page", "Page Access", "Page.UserManagement", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", "Test Operations", "Action.TestConfig.Edit", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", "System & Logs", "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup" });

            migrationBuilder.CreateIndex(
                name: "IX_TestConfigNets_ConfigurationConfigId",
                table: "TestConfigNets",
                column: "ConfigurationConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestConfigNets_TestConfigurations_ConfigurationConfigId",
                table: "TestConfigNets",
                column: "ConfigurationConfigId",
                principalTable: "TestConfigurations",
                principalColumn: "ConfigId");
        }
    }
}
