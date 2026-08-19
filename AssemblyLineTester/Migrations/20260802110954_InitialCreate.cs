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
                    RemainingMaintHours = table.Column<double>(type: "REAL", nullable: false),
                    IntervalDays = table.Column<int>(type: "INTEGER", nullable: false),
                    IntervalHours = table.Column<double>(type: "REAL", nullable: false)
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
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxWireResistance = table.Column<double>(type: "REAL", nullable: false),
                    MinComponentResistance = table.Column<double>(type: "REAL", nullable: false),
                    MinInsulationResistance = table.Column<double>(type: "REAL", nullable: false),
                    HipotVoltage = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    TestTypeValue = table.Column<int>(type: "INTEGER", nullable: false)
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
                    DefaultMinInulationResistance = table.Column<double>(type: "REAL", nullable: false),
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
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    TestTypeValue = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Tolerance = table.Column<double>(type: "REAL", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    Gauge = table.Column<string>(type: "TEXT", nullable: false)
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
                    FromLabel = table.Column<string>(type: "TEXT", nullable: false),
                    ToLabel = table.Column<string>(type: "TEXT", nullable: false),
                    TestTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    MeasuredValue = table.Column<double>(type: "REAL", nullable: true),
                    LowerLimit = table.Column<double>(type: "REAL", nullable: true),
                    UpperLimit = table.Column<double>(type: "REAL", nullable: true),
                    ExpectedValue = table.Column<double>(type: "REAL", nullable: true),
                    Tolerance = table.Column<double>(type: "REAL", nullable: true),
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
                table: "Areas",
                columns: new[] { "Id", "Desc", "Name" },
                values: new object[] { 1, null, "Riser Cabinet" });

            migrationBuilder.InsertData(
                table: "MaintenanceStates",
                columns: new[] { "Id", "IntervalDays", "IntervalHours", "LastUsageDate", "OperatingSecondsToday", "RemainingMaintDays", "RemainingMaintHours" },
                values: new object[] { 1, 180, 2160.0, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, 180, 2160.0 });

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
                table: "TestResultChecks",
                columns: new[] { "Id", "IsChecked", "TestResultName" },
                values: new object[,]
                {
                    { 1, true, "Net-ID" },
                    { 2, true, "Harness Group" },
                    { 3, true, "From" },
                    { 4, true, "Net From Label" },
                    { 5, true, "To" },
                    { 6, true, "Net To Label" },
                    { 7, true, "Test Type" },
                    { 8, true, "Lower Limit" },
                    { 9, true, "Upper Limit" },
                    { 10, true, "Expected Value" },
                    { 11, true, "Tolerance" },
                    { 12, true, "Measurement" },
                    { 13, true, "Color" },
                    { 14, true, "Gauge" }
                });

            migrationBuilder.InsertData(
                table: "TestTypeChecks",
                columns: new[] { "Id", "DisplayName", "IsChecked", "TestTypeName" },
                values: new object[,]
                {
                    { 1, "4RESISTOR", true, "4RESISTOR" },
                    { 2, "4WIRE", true, "4WIRE" },
                    { 3, "CAPACITOR", true, "CAPACITOR" },
                    { 4, "CLOSE", true, "CLOSE" },
                    { 5, "COMMENT", true, "COMMENT" },
                    { 6, "DIODE", true, "DIODE" },
                    { 7, "LINK", true, "LINK" },
                    { 8, "LUA COMPONENT", true, "LUA COMPONENT" },
                    { 9, "OPEN", true, "OPEN" },
                    { 10, "PROBE", true, "PROBE" },
                    { 11, "RESISTOR", true, "RESISTOR" },
                    { 12, "SIGNATURE DIODE", true, "SIGNATURE DIODE" },
                    { 13, "SIGNATURE WIRE", true, "SIGNATURE WIRE" },
                    { 14, "TWISTED PAIR", true, "TWISTED PAIR" },
                    { 15, "WIRE", true, "WIRE" }
                });

            migrationBuilder.InsertData(
                table: "TestTypes",
                columns: new[] { "Id", "DisplayName", "TestTypeName", "TestTypeValue" },
                values: new object[,]
                {
                    { 1, "4RESISTOR", "4RESISTOR", 0 },
                    { 2, "4WIRE", "4WIRE", 0 },
                    { 3, "CAPACITOR", "CAPACITOR", 2 },
                    { 4, "CLOSE", "CLOSE", 0 },
                    { 5, "COMMENT", "COMMENT", 0 },
                    { 6, "DIODE", "DIODE", 4 },
                    { 7, "LINK", "LINK", 0 },
                    { 8, "LUA COMPONENT", "LUA COMPONENT", 0 },
                    { 9, "OPEN", "OPEN", 0 },
                    { 10, "PROBE", "PROBE", 0 },
                    { 11, "RESISTOR", "RESISTOR", 1 },
                    { 12, "SIGNATURE DIODE", "SIGNATURE DIODE", 0 },
                    { 13, "SIGNATURE WIRE", "SIGNATURE WIRE", 0 },
                    { 14, "TWISTED PAIR", "TWISTED PAIR", 8 },
                    { 15, "WIRE", "WIRE", 0 }
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
                table: "LearnConnections",
                columns: new[] { "Id", "DisplayName", "IsChecked", "TestTypeId", "TestTypeValue" },
                values: new object[,]
                {
                    { 1, "Wire", true, 15, 0 },
                    { 2, "Resistance", true, 11, 0 },
                    { 3, "Capacitor", true, 3, 0 },
                    { 4, "Diode", true, 6, 0 },
                    { 5, "Twisted Pair", true, 14, 0 }
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
                    { 18, "Reset the Maintenance Countdown", true, "System & Logs", "Action.Maintenance.Reset", 1 },
                    { 19, "Add, Edit, and Delete Harnesses and Connectors", true, "Test Operations", "Action.HarnessDatabase.Edit", 1 },
                    { 20, "Change Default Test Settings", true, "System & Logs", "Action.Settings.Edit", 1 },
                    { 21, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 2 },
                    { 22, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 2 },
                    { 23, "Access Harness Database Page", false, "Page Access", "Page.HarnessDatabase", 2 },
                    { 24, "Access Test History Page", true, "Page Access", "Page.TestHistory", 2 },
                    { 25, "Access Reports Page", false, "Page Access", "Page.Reports", 2 },
                    { 26, "Access System Log Page", false, "Page Access", "Page.SystemLog", 2 },
                    { 27, "Access Settings Page", false, "Page Access", "Page.Settings", 2 },
                    { 28, "Access User Management Page", false, "Page Access", "Page.UserManagement", 2 },
                    { 29, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 2 },
                    { 30, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 2 },
                    { 31, "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate", 2 },
                    { 32, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 2 },
                    { 33, "Export and Print Test Reports & Analytics", false, "System & Logs", "Action.Reports.Export", 2 },
                    { 34, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 2 },
                    { 35, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 2 },
                    { 36, "Amend and Delete Saved Test Reports", false, "System & Logs", "Action.Reports.Edit", 2 },
                    { 37, "Delete Test History Records", false, "System & Logs", "Action.TestHistory.Delete", 2 },
                    { 38, "Reset the Maintenance Countdown", false, "System & Logs", "Action.Maintenance.Reset", 2 },
                    { 39, "Add, Edit, and Delete Harnesses and Connectors", false, "Test Operations", "Action.HarnessDatabase.Edit", 2 },
                    { 40, "Change Default Test Settings", false, "System & Logs", "Action.Settings.Edit", 2 },
                    { 41, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 3 },
                    { 42, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 3 },
                    { 43, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 3 },
                    { 44, "Access Test History Page", true, "Page Access", "Page.TestHistory", 3 },
                    { 45, "Access Reports Page", true, "Page Access", "Page.Reports", 3 },
                    { 46, "Access System Log Page", true, "Page Access", "Page.SystemLog", 3 },
                    { 47, "Access Settings Page", true, "Page Access", "Page.Settings", 3 },
                    { 48, "Access User Management Page", false, "Page Access", "Page.UserManagement", 3 },
                    { 49, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 3 },
                    { 50, "Add and Edit Test Parameters & Harness Specs", true, "Test Operations", "Action.TestConfig.Edit", 3 },
                    { 51, "Calibrate and Configure Hardware Instruments", true, "Test Operations", "Action.Instruments.Calibrate", 3 },
                    { 52, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 3 },
                    { 53, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 3 },
                    { 54, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 3 },
                    { 55, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 3 },
                    { 56, "Amend and Delete Saved Test Reports", true, "System & Logs", "Action.Reports.Edit", 3 },
                    { 57, "Delete Test History Records", true, "System & Logs", "Action.TestHistory.Delete", 3 },
                    { 58, "Reset the Maintenance Countdown", true, "System & Logs", "Action.Maintenance.Reset", 3 },
                    { 59, "Add, Edit, and Delete Harnesses and Connectors", true, "Test Operations", "Action.HarnessDatabase.Edit", 3 },
                    { 60, "Change Default Test Settings", true, "System & Logs", "Action.Settings.Edit", 3 },
                    { 61, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 4 },
                    { 62, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 4 },
                    { 63, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 4 },
                    { 64, "Access Test History Page", true, "Page Access", "Page.TestHistory", 4 },
                    { 65, "Access Reports Page", false, "Page Access", "Page.Reports", 4 },
                    { 66, "Access System Log Page", true, "Page Access", "Page.SystemLog", 4 },
                    { 67, "Access Settings Page", true, "Page Access", "Page.Settings", 4 },
                    { 68, "Access User Management Page", false, "Page Access", "Page.UserManagement", 4 },
                    { 69, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 4 },
                    { 70, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 4 },
                    { 71, "Calibrate and Configure Hardware Instruments", true, "Test Operations", "Action.Instruments.Calibrate", 4 },
                    { 72, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 4 },
                    { 73, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 4 },
                    { 74, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 4 },
                    { 75, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 4 },
                    { 76, "Amend and Delete Saved Test Reports", false, "System & Logs", "Action.Reports.Edit", 4 },
                    { 77, "Delete Test History Records", false, "System & Logs", "Action.TestHistory.Delete", 4 },
                    { 78, "Reset the Maintenance Countdown", true, "System & Logs", "Action.Maintenance.Reset", 4 },
                    { 79, "Add, Edit, and Delete Harnesses and Connectors", true, "Test Operations", "Action.HarnessDatabase.Edit", 4 },
                    { 80, "Change Default Test Settings", false, "System & Logs", "Action.Settings.Edit", 4 },
                    { 81, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 5 },
                    { 82, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 5 },
                    { 83, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 5 },
                    { 84, "Access Test History Page", true, "Page Access", "Page.TestHistory", 5 },
                    { 85, "Access Reports Page", true, "Page Access", "Page.Reports", 5 },
                    { 86, "Access System Log Page", false, "Page Access", "Page.SystemLog", 5 },
                    { 87, "Access Settings Page", false, "Page Access", "Page.Settings", 5 },
                    { 88, "Access User Management Page", false, "Page Access", "Page.UserManagement", 5 },
                    { 89, "Start and Run Testing Operations", false, "Test Operations", "Action.TestExecution.Run", 5 },
                    { 90, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 5 },
                    { 91, "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate", 5 },
                    { 92, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 5 },
                    { 93, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 5 },
                    { 94, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 5 },
                    { 95, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 5 },
                    { 96, "Amend and Delete Saved Test Reports", false, "System & Logs", "Action.Reports.Edit", 5 },
                    { 97, "Delete Test History Records", false, "System & Logs", "Action.TestHistory.Delete", 5 },
                    { 98, "Reset the Maintenance Countdown", false, "System & Logs", "Action.Maintenance.Reset", 5 },
                    { 99, "Add, Edit, and Delete Harnesses and Connectors", false, "Test Operations", "Action.HarnessDatabase.Edit", 5 },
                    { 100, "Change Default Test Settings", false, "System & Logs", "Action.Settings.Edit", 5 },
                    { 101, "Access Dashboard Page", true, "Page Access", "Page.Dashboard", 6 },
                    { 102, "Access Test Execution Page", true, "Page Access", "Page.TestExecution", 6 },
                    { 103, "Access Harness Database Page", true, "Page Access", "Page.HarnessDatabase", 6 },
                    { 104, "Access Test History Page", true, "Page Access", "Page.TestHistory", 6 },
                    { 105, "Access Reports Page", true, "Page Access", "Page.Reports", 6 },
                    { 106, "Access System Log Page", true, "Page Access", "Page.SystemLog", 6 },
                    { 107, "Access Settings Page", false, "Page Access", "Page.Settings", 6 },
                    { 108, "Access User Management Page", false, "Page Access", "Page.UserManagement", 6 },
                    { 109, "Start and Run Testing Operations", true, "Test Operations", "Action.TestExecution.Run", 6 },
                    { 110, "Add and Edit Test Parameters & Harness Specs", false, "Test Operations", "Action.TestConfig.Edit", 6 },
                    { 111, "Calibrate and Configure Hardware Instruments", false, "Test Operations", "Action.Instruments.Calibrate", 6 },
                    { 112, "Add, Edit, and Manage System User Accounts", false, "System & Logs", "Action.UserManagement.Edit", 6 },
                    { 113, "Export and Print Test Reports & Analytics", true, "System & Logs", "Action.Reports.Export", 6 },
                    { 114, "Clear and Purge System Activity Logs", false, "System & Logs", "Action.SystemLog.Clear", 6 },
                    { 115, "Create and Restore Database Backups", false, "System & Logs", "Action.Database.Backup", 6 },
                    { 116, "Amend and Delete Saved Test Reports", true, "System & Logs", "Action.Reports.Edit", 6 },
                    { 117, "Delete Test History Records", true, "System & Logs", "Action.TestHistory.Delete", 6 },
                    { 118, "Reset the Maintenance Countdown", false, "System & Logs", "Action.Maintenance.Reset", 6 },
                    { 119, "Add, Edit, and Delete Harnesses and Connectors", false, "Test Operations", "Action.HarnessDatabase.Edit", 6 },
                    { 120, "Change Default Test Settings", false, "System & Logs", "Action.Settings.Edit", 6 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CompanyId", "CreatedAt", "Department", "FailedLoginCount", "FullName", "LastLogin", "LockoutUntil", "MustChangePassword", "PasswordHash", "RoleId", "StatusId", "UpdatedAt", "Username" },
                values: new object[] { 1, "CAT0100", new DateTime(2026, 3, 1, 15, 0, 0, 0, DateTimeKind.Local), "Engineering", 0, "System Administrator", null, null, true, "$2a$11$Hcpu0fJL6xvHgmbYJNv/s.QMrlL8F9ALRHip20cUQLY6OSro3YIhC", 1, 1, new DateTime(2026, 3, 1, 15, 0, 0, 0, DateTimeKind.Local), "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Name",
                table: "Areas",
                column: "Name",
                unique: true);

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
                name: "IX_ProductModels_Name",
                table: "ProductModels",
                column: "Name",
                unique: true);

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
                name: "IX_TestConfigurationTestType_TestTypesId",
                table: "TestConfigurationTestType",
                column: "TestTypesId");

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
                name: "Areas");

            migrationBuilder.DropTable(
                name: "DefaultTestSettings");

            migrationBuilder.DropTable(
                name: "HarnessConnectors");

            migrationBuilder.DropTable(
                name: "LearnConnections");

            migrationBuilder.DropTable(
                name: "MaintenanceStates");

            migrationBuilder.DropTable(
                name: "ProductModels");

            migrationBuilder.DropTable(
                name: "ReportTestTypeSummaries");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SystemLogs");

            migrationBuilder.DropTable(
                name: "TestConfigNets");

            migrationBuilder.DropTable(
                name: "TestConfigurationTestType");

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
