using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssemblyLineTester.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Connectors",
                columns: table => new
                {
                    ConnectorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Perf = table.Column<string>(type: "TEXT", nullable: false),
                    PartNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ContactType = table.Column<string>(type: "TEXT", nullable: false),
                    ContactQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    WireSize = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentRating = table.Column<double>(type: "REAL", nullable: false),
                    MountingStyle = table.Column<string>(type: "TEXT", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connectors", x => x.ConnectorId);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastUsageDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OperatingSecondsToday = table.Column<double>(type: "REAL", nullable: false),
                    RemainingMaintDays = table.Column<int>(type: "INTEGER", nullable: false),
                    RemainingMaintHours = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductHarnesses",
                columns: table => new
                {
                    ProductHarnessId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ProductModel = table.Column<string>(type: "TEXT", nullable: false),
                    ProductType = table.Column<string>(type: "TEXT", nullable: false),
                    Area = table.Column<string>(type: "TEXT", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ImagePaths = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHarnesses", x => x.ProductHarnessId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SeverityLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    ModuleName = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    ActionName = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    IPAddress = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestConfigurations",
                columns: table => new
                {
                    ConfigId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                name: "TestReports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ProductModel = table.Column<string>(type: "TEXT", nullable: false),
                    ProductType = table.Column<string>(type: "TEXT", nullable: false),
                    Area = table.Column<string>(type: "TEXT", nullable: false),
                    ECNNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Revision = table.Column<string>(type: "TEXT", nullable: false),
                    PartNumber = table.Column<string>(type: "TEXT", nullable: false),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Operator = table.Column<string>(type: "TEXT", nullable: false),
                    TestDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalNets = table.Column<int>(type: "INTEGER", nullable: false),
                    PassCount = table.Column<int>(type: "INTEGER", nullable: false),
                    FailCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    OverallResult = table.Column<int>(type: "INTEGER", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    IsDraft = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestReports", x => x.ReportId);
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

            migrationBuilder.CreateTable(
                name: "TestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypes", x => x.Id);
                    table.UniqueConstraint("AK_TestTypes_TestTypeName", x => x.TestTypeName);
                });

            migrationBuilder.CreateTable(
                name: "UserStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HarnessConnectors",
                columns: table => new
                {
                    HarnessConnectorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductHarnessId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConnectorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarnessConnectors", x => x.HarnessConnectorId);
                    table.ForeignKey(
                        name: "FK_HarnessConnectors_Connectors_ConnectorId",
                        column: x => x.ConnectorId,
                        principalTable: "Connectors",
                        principalColumn: "ConnectorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HarnessConnectors_ProductHarnesses_ProductHarnessId",
                        column: x => x.ProductHarnessId,
                        principalTable: "ProductHarnesses",
                        principalColumn: "ProductHarnessId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "ReportTestTypeSummaries",
                columns: table => new
                {
                    SummaryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    TotalCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PassCount = table.Column<int>(type: "INTEGER", nullable: false),
                    FailCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTestTypeSummaries", x => x.SummaryId);
                    table.ForeignKey(
                        name: "FK_ReportTestTypeSummaries_TestReports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "TestReports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportTestTypeSummaries_TestTypes_TestTypeName",
                        column: x => x.TestTypeName,
                        principalTable: "TestTypes",
                        principalColumn: "TestTypeName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestConfigNets",
                columns: table => new
                {
                    NetConfigId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigId = table.Column<int>(type: "INTEGER", nullable: false),
                    NetId = table.Column<string>(type: "TEXT", nullable: false),
                    SequenceNo = table.Column<int>(type: "INTEGER", nullable: false),
                    HarnessGroup = table.Column<string>(type: "TEXT", nullable: false),
                    FromAddress = table.Column<int>(type: "INTEGER", nullable: false),
                    FromLabel = table.Column<string>(type: "TEXT", nullable: false),
                    ToAddress = table.Column<int>(type: "INTEGER", nullable: false),
                    ToLabel = table.Column<string>(type: "TEXT", nullable: false),
                    TestTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    LowerLimit = table.Column<double>(type: "REAL", nullable: true),
                    UpperLimit = table.Column<double>(type: "REAL", nullable: true),
                    MeasurementUnit = table.Column<string>(type: "TEXT", nullable: false),
                    ExpectedValue = table.Column<double>(type: "REAL", nullable: true),
                    Tolerance = table.Column<double>(type: "REAL", nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestDetails",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportId = table.Column<int>(type: "INTEGER", nullable: false),
                    NetId = table.Column<string>(type: "TEXT", nullable: false),
                    HarnessGroup = table.Column<string>(type: "TEXT", nullable: false),
                    FromPin = table.Column<string>(type: "TEXT", nullable: false),
                    ToPin = table.Column<string>(type: "TEXT", nullable: false),
                    TestTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    MeasuredValue = table.Column<double>(type: "REAL", nullable: true),
                    LowerLimit = table.Column<double>(type: "REAL", nullable: true),
                    UpperLimit = table.Column<double>(type: "REAL", nullable: true),
                    Unit = table.Column<string>(type: "TEXT", nullable: false),
                    Result = table.Column<bool>(type: "INTEGER", nullable: false),
                    ErrorDetail = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDetails", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_TestDetails_TestReports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "TestReports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestDetails_TestTypes_TestTypeName",
                        column: x => x.TestTypeName,
                        principalTable: "TestTypes",
                        principalColumn: "TestTypeName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestSteps",
                columns: table => new
                {
                    StepId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportId = table.Column<int>(type: "INTEGER", nullable: false),
                    StepNo = table.Column<int>(type: "INTEGER", nullable: false),
                    TestTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    Result = table.Column<bool>(type: "INTEGER", nullable: false),
                    DurationSeconds = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSteps", x => x.StepId);
                    table.ForeignKey(
                        name: "FK_TestSteps_TestReports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "TestReports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestSteps_TestTypes_TestTypeName",
                        column: x => x.TestTypeName,
                        principalTable: "TestTypes",
                        principalColumn: "TestTypeName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    MustChangePassword = table.Column<bool>(type: "INTEGER", nullable: false),
                    FailedLoginCount = table.Column<int>(type: "INTEGER", nullable: false),
                    LockoutUntil = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_UserStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "UserStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MaintenanceStates",
                columns: new[] { "Id", "LastUsageDate", "OperatingSecondsToday", "RemainingMaintDays", "RemainingMaintHours" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, 180, 2160.0 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Full system administration access", "Administrator" },
                    { 2, "Standard machine line operations", "Operator" },
                    { 3, "System configuration and engineering access", "Engineer" },
                    { 4, "Hardware maintenance and troubleshooting access", "Technician" },
                    { 5, "Quality assurance inspection access", "Quality" },
                    { 6, "Line shift supervision access", "Supervisor" }
                });

            migrationBuilder.InsertData(
                table: "TestTypes",
                columns: new[] { "Id", "DisplayName", "TestTypeName" },
                values: new object[,]
                {
                    { 1, "4RESISTOR", "4RESISTOR" },
                    { 2, "4WIRE", "4WIRE" },
                    { 3, "CAPACITOR", "CAPACITOR" },
                    { 4, "CLOSE", "CLOSE" },
                    { 5, "COMMENT", "COMMENT" },
                    { 6, "DIODE", "DIODE" },
                    { 7, "LINK", "LINK" },
                    { 8, "LUA COMPONENT", "LUA COMPONENT" },
                    { 9, "OPEN", "OPEN" },
                    { 10, "PROBE", "PROBE" },
                    { 11, "RESISTOR", "RESISTOR" },
                    { 12, "SIGNATURE DIODE", "SIGNATURE DIODE" },
                    { 13, "SIGNATURE WIRE", "SIGNATURE WIRE" },
                    { 14, "TWISTED PAIR", "TWISTED PAIR" },
                    { 15, "WIRE", "WIRE" }
                });

            migrationBuilder.InsertData(
                table: "UserStatuses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "User is active and allowed to log in", "Active" },
                    { 2, "User is inactive and disabled", "Inactive" },
                    { 3, "User is locked out due to multiple failed login attempts", "Locked" }
                });

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
                    { 16, "Amend and Delete Saved Test Reports", true, "System & Logs", "Action.Reports.Edit", 1 },
                    { 17, "Delete Test History Records", true, "System & Logs", "Action.TestHistory.Delete", 1 },
                    { 18, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 2 },
                    { 19, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 2 },
                    { 20, "Access Harness Database Page", false, "Page Access", "Page.HarnessDatabase", 2 },
                    { 21, "Access Test History Page", true, "Page Access", "Page.TestHistory", 2 },
                    { 22, "Access Reports Page", false, "Page Access", "Page.Reports", 2 },
                    { 23, "Access System Log Page", false, "Page Access", "Page.SystemLog", 2 },
                    { 24, "Access Settings Page", false, "Page Access", "Page.Settings", 2 },
                    { 25, "Access User Management Page", false, "Page Access", "Page.UserManagement", 2 },
                    { 26, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 2 },
                    { 27, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 2 },
                    { 28, "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate", 2 },
                    { 29, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 2 },
                    { 30, "Export and Print Test Reports & Analytics", false, "System & Logs", "Action.Reports.Export", 2 },
                    { 31, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 2 },
                    { 32, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 2 },
                    { 33, "Amend and Delete Saved Test Reports", false, "System & Logs", "Action.Reports.Edit", 2 },
                    { 34, "Delete Test History Records", false, "System & Logs", "Action.TestHistory.Delete", 2 },
                    { 35, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 3 },
                    { 36, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 3 },
                    { 37, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 3 },
                    { 38, "Access Test History Page", true, "Page Access", "Page.TestHistory", 3 },
                    { 39, "Access Reports Page", true, "Page Access", "Page.Reports", 3 },
                    { 40, "Access System Log Page", true, "Page Access", "Page.SystemLog", 3 },
                    { 41, "Access Settings Page", true, "Page Access", "Page.Settings", 3 },
                    { 42, "Access User Management Page", false, "Page Access", "Page.UserManagement", 3 },
                    { 43, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 3 },
                    { 44, "Add and Edit Test Parameters & Harness Specs", true, "Test Operations", "Action.TestConfig.Edit", 3 },
                    { 45, "Calibrate and Configure Hardware Instruments", true, "Test Operations", "Action.Instruments.Calibrate", 3 },
                    { 46, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 3 },
                    { 47, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 3 },
                    { 48, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 3 },
                    { 49, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 3 },
                    { 50, "Amend and Delete Saved Test Reports", true, "System & Logs", "Action.Reports.Edit", 3 },
                    { 51, "Delete Test History Records", true, "System & Logs", "Action.TestHistory.Delete", 3 },
                    { 52, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 4 },
                    { 53, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 4 },
                    { 54, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 4 },
                    { 55, "Access Test History Page", true, "Page Access", "Page.TestHistory", 4 },
                    { 56, "Access Reports Page", false, "Page Access", "Page.Reports", 4 },
                    { 57, "Access System Log Page", true, "Page Access", "Page.SystemLog", 4 },
                    { 58, "Access Settings Page", true, "Page Access", "Page.Settings", 4 },
                    { 59, "Access User Management Page", false, "Page Access", "Page.UserManagement", 4 },
                    { 60, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 4 },
                    { 61, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 4 },
                    { 62, "Calibrate and Configure Hardware Instruments", true, "Test Operations", "Action.Instruments.Calibrate", 4 },
                    { 63, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 4 },
                    { 64, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 4 },
                    { 65, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 4 },
                    { 66, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 4 },
                    { 67, "Amend and Delete Saved Test Reports", false, "System & Logs", "Action.Reports.Edit", 4 },
                    { 68, "Delete Test History Records", false, "System & Logs", "Action.TestHistory.Delete", 4 },
                    { 69, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 5 },
                    { 70, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 5 },
                    { 71, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 5 },
                    { 72, "Access Test History Page", true, "Page Access", "Page.TestHistory", 5 },
                    { 73, "Access Reports Page", true, "Page Access", "Page.Reports", 5 },
                    { 74, "Access System Log Page", false, "Page Access", "Page.SystemLog", 5 },
                    { 75, "Access Settings Page", false, "Page Access", "Page.Settings", 5 },
                    { 76, "Access User Management Page", false, "Page Access", "Page.UserManagement", 5 },
                    { 77, "Start and Run Testing Operations", false, "Test Operations", "Action.TestExecution.Run", 5 },
                    { 78, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 5 },
                    { 79, "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate", 5 },
                    { 80, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 5 },
                    { 81, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 5 },
                    { 82, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 5 },
                    { 83, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 5 },
                    { 84, "Amend and Delete Saved Test Reports", false, "System & Logs", "Action.Reports.Edit", 5 },
                    { 85, "Delete Test History Records", false, "System & Logs", "Action.TestHistory.Delete", 5 },
                    { 86, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 6 },
                    { 87, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 6 },
                    { 88, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 6 },
                    { 89, "Access Test History Page", true, "Page Access", "Page.TestHistory", 6 },
                    { 90, "Access Reports Page", true, "Page Access", "Page.Reports", 6 },
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CompanyId", "CreatedAt", "Department", "FailedLoginCount", "FullName", "LastLogin", "LockoutUntil", "MustChangePassword", "PasswordHash", "RoleId", "StatusId", "UpdatedAt", "Username" },
                values: new object[] { 1, "CAT0100", new DateTime(2026, 3, 1, 15, 0, 0, 0, DateTimeKind.Local), "Engineering", 0, "System Administrator", null, null, true, "$2a$11$Hcpu0fJL6xvHgmbYJNv/s.QMrlL8F9ALRHip20cUQLY6OSro3YIhC", 1, 1, new DateTime(2026, 3, 1, 15, 0, 0, 0, DateTimeKind.Local), "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_DefaultTestSettings_DefaultTestTypeId",
                table: "DefaultTestSettings",
                column: "DefaultTestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HarnessConnectors_ConnectorId",
                table: "HarnessConnectors",
                column: "ConnectorId");

            migrationBuilder.CreateIndex(
                name: "IX_HarnessConnectors_ProductHarnessId_ConnectorId",
                table: "HarnessConnectors",
                columns: new[] { "ProductHarnessId", "ConnectorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearnConnections_TestTypeId",
                table: "LearnConnections",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTestTypeSummaries_ReportId_TestTypeName",
                table: "ReportTestTypeSummaries",
                columns: new[] { "ReportId", "TestTypeName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportTestTypeSummaries_TestTypeName",
                table: "ReportTestTypeSummaries",
                column: "TestTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId_PermissionKey",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemLogs_Timestamp",
                table: "SystemLogs",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_TestConfigNets_ConfigId",
                table: "TestConfigNets",
                column: "ConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_TestConfigNets_TestTypeId",
                table: "TestConfigNets",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDetails_ReportId",
                table: "TestDetails",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDetails_TestTypeName",
                table: "TestDetails",
                column: "TestTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_TestReports_ReportNumber",
                table: "TestReports",
                column: "ReportNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestReports_TestDateTime",
                table: "TestReports",
                column: "TestDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_TestSteps_ReportId_StepNo",
                table: "TestSteps",
                columns: new[] { "ReportId", "StepNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestSteps_TestTypeName",
                table: "TestSteps",
                column: "TestTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypes_TestTypeName",
                table: "TestTypes",
                column: "TestTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId",
                unique: true,
                filter: "\"CompanyId\" <> ''");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatusId",
                table: "Users",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStatuses_Name",
                table: "UserStatuses",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultTestSettings");

            migrationBuilder.DropTable(
                name: "HarnessConnectors");

            migrationBuilder.DropTable(
                name: "LearnConnections");

            migrationBuilder.DropTable(
                name: "MaintenanceStates");

            migrationBuilder.DropTable(
                name: "ReportTestTypeSummaries");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SystemLogs");

            migrationBuilder.DropTable(
                name: "TestConfigNets");

            migrationBuilder.DropTable(
                name: "TestDetails");

            migrationBuilder.DropTable(
                name: "TestResultChecks");

            migrationBuilder.DropTable(
                name: "TestSteps");

            migrationBuilder.DropTable(
                name: "TestTypeChecks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Connectors");

            migrationBuilder.DropTable(
                name: "ProductHarnesses");

            migrationBuilder.DropTable(
                name: "TestConfigurations");

            migrationBuilder.DropTable(
                name: "TestReports");

            migrationBuilder.DropTable(
                name: "TestTypes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserStatuses");
        }
    }
}
