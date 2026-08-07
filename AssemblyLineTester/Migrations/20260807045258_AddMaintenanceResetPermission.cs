using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class AddMaintenanceResetPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Reset the Maintenance Countdown", "System & Logs", "Action.Maintenance.Reset", 1 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Dashboard Page", "Page.Dashboard" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Test Execution Page", true, "Page.TestExecution" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", false, "Page.HarnessDatabase" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Test History Page", true, "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Reports Page", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access System Log Page", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access User Management Page", false, "Page Access", "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", true, "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", "Action.Database.Backup" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Amend and Delete Saved Test Reports", "Action.Reports.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Delete Test History Records", false, "System & Logs", "Action.TestHistory.Delete", 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Reset the Maintenance Countdown", false, "System & Logs", "Action.Maintenance.Reset", 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Dashboard Page", "Page.Dashboard" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Test Execution Page", "Page.TestExecution" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", "Page.HarnessDatabase" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Test History Page", "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Reports Page", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access System Log Page", true, "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page Access", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access User Management Page", false, "Page Access", "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", true, "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", true, "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", false, "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", false, "Action.Database.Backup" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Amend and Delete Saved Test Reports", "System & Logs", "Action.Reports.Edit", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Delete Test History Records", "System & Logs", "Action.TestHistory.Delete", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Reset the Maintenance Countdown", "System & Logs", "Action.Maintenance.Reset", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Dashboard Page", "Page.Dashboard" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Test Execution Page", true, "Page.TestExecution" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", "Page.HarnessDatabase" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Test History Page", "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Reports Page", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access System Log Page", "Page Access", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Settings Page", true, "Page Access", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access User Management Page", false, "Page Access", "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", true, "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", true, "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Amend and Delete Saved Test Reports", false, "System & Logs", "Action.Reports.Edit", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Delete Test History Records", false, "System & Logs", "Action.TestHistory.Delete", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Reset the Maintenance Countdown", "System & Logs", "Action.Maintenance.Reset", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Dashboard Page", "Page.Dashboard" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Test Execution Page", true, "Page.TestExecution" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", true, "Page.HarnessDatabase" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Test History Page", true, "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Reports Page", true, "Page Access", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access System Log Page", "Page Access", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page Access", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access User Management Page", "Page Access", "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", false, "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", true, "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Amend and Delete Saved Test Reports", false, "System & Logs", "Action.Reports.Edit", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Delete Test History Records", false, "System & Logs", "Action.TestHistory.Delete", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Reset the Maintenance Countdown", false, "System & Logs", "Action.Maintenance.Reset", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Dashboard Page", "Page.Dashboard" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Test Execution Page", true, "Page.TestExecution" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", true, "Page.HarnessDatabase" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Test History Page", "Page Access", "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Reports Page", true, "Page Access", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access System Log Page", true, "Page Access", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page Access", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Access User Management Page", false, "Page Access", "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", false, "Action.UserManagement.Edit" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[,]
                {
                    { 103, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 6 },
                    { 104, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 6 },
                    { 105, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 6 },
                    { 106, "Amend and Delete Saved Test Reports", true, "System & Logs", "Action.Reports.Edit", 6 },
                    { 107, "Delete Test History Records", true, "System & Logs", "Action.TestHistory.Delete", 6 },
                    { 108, "Reset the Maintenance Countdown", false, "System & Logs", "Action.Maintenance.Reset", 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Dashboard Page", "Page Access", "Page.Dashboard", 2 });

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
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Harness Database Page", false, "Page.HarnessDatabase" });

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
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Reports Page", false, "Page.Reports" });

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
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access User Management Page", "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", false, "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", "System & Logs", "Action.UserManagement.Edit" });

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
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", "Action.Database.Backup" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Amend and Delete Saved Test Reports", "Action.Reports.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Delete Test History Records", "Action.TestHistory.Delete" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 3 });

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
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Test History Page", "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Reports Page", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access System Log Page", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access User Management Page", false, "Page.UserManagement" });

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
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", "System & Logs", "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", false, "Action.Database.Backup" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Amend and Delete Saved Test Reports", true, "Action.Reports.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Delete Test History Records", true, "Action.TestHistory.Delete" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Dashboard Page", "Page Access", "Page.Dashboard", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test Execution Page", "Page Access", "Page.TestExecution", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Harness Database Page", "Page Access", "Page.HarnessDatabase", 4 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Test History Page", "Page.TestHistory" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Reports Page", false, "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access System Log Page", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Settings Page", "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access User Management Page", "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", true, "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", "Action.Database.Backup" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Amend and Delete Saved Test Reports", false, "Action.Reports.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Delete Test History Records", "Action.TestHistory.Delete" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test History Page", "Page Access", "Page.TestHistory", 5 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access Reports Page", "Page.Reports" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access System Log Page", false, "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Settings Page", false, "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access User Management Page", false, "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", false, "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", "System & Logs", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", "System & Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", "System & Logs", "Action.Database.Backup" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Amend and Delete Saved Test Reports", "Action.Reports.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Delete Test History Records", false, "Action.TestHistory.Delete" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Test History Page", true, "Page Access", "Page.TestHistory", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey", "RoleId" },
                values: new object[] { "Access Reports Page", true, "Page Access", "Page.Reports", 6 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "Description", "PermissionKey" },
                values: new object[] { "Access System Log Page", "Page.SystemLog" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access Settings Page", false, "Page.Settings" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Access User Management Page", false, "Page.UserManagement" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Start and Run Testing Operations", "Test Operations", "Action.TestExecution.Run" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Add, Edit, and Manage System User Accounts", "System & Logs", "Action.UserManagement.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "Description", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Create and Restore Database Backups", "System & Logs", "Action.Database.Backup" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "Description", "IsGranted", "PermissionGroup", "PermissionKey" },
                values: new object[] { "Amend and Delete Saved Test Reports", true, "System & Logs", "Action.Reports.Edit" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "Description", "IsGranted", "PermissionKey" },
                values: new object[] { "Delete Test History Records", true, "Action.TestHistory.Delete" });
        }
    }
}
