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
                    TesterModel = table.Column<string>(type: "TEXT", nullable: false),
                    TestDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalNets = table.Column<int>(type: "INTEGER", nullable: false),
                    PassCount = table.Column<int>(type: "INTEGER", nullable: false),
                    FailCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    OverallResult = table.Column<int>(type: "INTEGER", nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestReports", x => x.ReportId);
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
                name: "TestConfigNets",
                columns: table => new
                {
                    NetConfigId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConfigurationConfigId = table.Column<int>(type: "INTEGER", nullable: false),
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
                        name: "FK_TestConfigNets_TestConfigurations_ConfigurationConfigId",
                        column: x => x.ConfigurationConfigId,
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
                    Color = table.Column<string>(type: "TEXT", nullable: false),
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
                values: new object[] { 1, new DateTime(2026, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), 0.0, 180, 2160.0 });

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
                table: "Users",
                columns: new[] { "UserId", "CompanyId", "CreatedAt", "Department", "FullName", "LastLogin", "PasswordHash", "RoleId", "StatusId", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, "CAT0100", new DateTime(2026, 3, 1, 15, 0, 0, 0, DateTimeKind.Local), "Engineering", "System Administrator", null, "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 1, 1, new DateTime(2026, 3, 1, 15, 0, 0, 0, DateTimeKind.Local), "admin" },
                    { 2, "CAT0001", new DateTime(2026, 3, 10, 9, 15, 22, 0, DateTimeKind.Unspecified), "Engineering", "Fally Kusuma", new DateTime(2026, 3, 15, 10, 28, 41, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 1, 1, new DateTime(2026, 3, 15, 10, 28, 41, 0, DateTimeKind.Unspecified), "fally12" },
                    { 3, "CAT0002", new DateTime(2026, 3, 11, 8, 23, 15, 0, DateTimeKind.Unspecified), "Engineering", "Faiz", new DateTime(2026, 3, 15, 9, 41, 12, 0, DateTimeKind.Unspecified), "$2b$11$rZFf4GHizQh2xGlxxdSj4eRzsOf4gSoVdwbLOcvwfozyn5nVfKd/2", 3, 1, new DateTime(2026, 3, 15, 9, 41, 12, 0, DateTimeKind.Unspecified), "faiz" },
                    { 4, "CAT0003", new DateTime(2026, 3, 12, 14, 10, 5, 0, DateTimeKind.Unspecified), "Production", "Doni Pratama", new DateTime(2026, 3, 15, 9, 55, 33, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 2, 1, new DateTime(2026, 3, 15, 9, 55, 33, 0, DateTimeKind.Unspecified), "doni42" },
                    { 5, "CAT0004", new DateTime(2026, 3, 12, 15, 45, 30, 0, DateTimeKind.Unspecified), "Maintenance", "Rian Hidayat", new DateTime(2026, 3, 14, 16, 22, 18, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 4, 1, new DateTime(2026, 3, 14, 16, 22, 18, 0, DateTimeKind.Unspecified), "rian24" },
                    { 6, "CAT0005", new DateTime(2026, 3, 13, 9, 2, 44, 0, DateTimeKind.Unspecified), "Production", "Maria Sari", new DateTime(2026, 3, 15, 8, 17, 9, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 2, 1, new DateTime(2026, 3, 15, 8, 17, 9, 0, DateTimeKind.Unspecified), "maria57" },
                    { 7, "CAT0006", new DateTime(2026, 3, 13, 11, 21, 36, 0, DateTimeKind.Unspecified), "Engineering", "Agus Setiawan", new DateTime(2026, 3, 15, 7, 58, 23, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 3, 1, new DateTime(2026, 3, 15, 7, 58, 23, 0, DateTimeKind.Unspecified), "agus08" },
                    { 8, "CAT0007", new DateTime(2026, 3, 14, 10, 5, 17, 0, DateTimeKind.Unspecified), "Quality Assurance", "Lina Putri", new DateTime(2026, 3, 14, 17, 11, 5, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 5, 1, new DateTime(2026, 3, 14, 17, 11, 5, 0, DateTimeKind.Unspecified), "lina15" },
                    { 9, "CAT0008", new DateTime(2026, 3, 14, 13, 17, 8, 0, DateTimeKind.Unspecified), "Maintenance", "Yudi Rahman", new DateTime(2026, 3, 14, 15, 33, 44, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 4, 1, new DateTime(2026, 3, 14, 15, 33, 44, 0, DateTimeKind.Unspecified), "yudi89" },
                    { 10, "CAT0009", new DateTime(2026, 3, 5, 9, 40, 10, 0, DateTimeKind.Unspecified), "Production", "Budi Santoso", new DateTime(2026, 3, 8, 14, 12, 33, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 6, 2, new DateTime(2026, 3, 8, 14, 12, 33, 0, DateTimeKind.Unspecified), "budi24" },
                    { 11, "CAT0010", new DateTime(2026, 3, 6, 14, 56, 31, 0, DateTimeKind.Unspecified), "Production", "Lisa Kartika", new DateTime(2026, 3, 7, 11, 7, 15, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 2, 2, new DateTime(2026, 3, 7, 11, 7, 15, 0, DateTimeKind.Unspecified), "lisa11" },
                    { 12, "CAT0011", new DateTime(2026, 3, 1, 8, 15, 0, 0, DateTimeKind.Unspecified), "Engineering", "Hendra Wijaya", new DateTime(2026, 3, 6, 10, 15, 0, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 3, 1, new DateTime(2026, 3, 6, 10, 15, 0, 0, DateTimeKind.Unspecified), "hendra655" },
                    { 13, "CAT0012", new DateTime(2026, 3, 1, 9, 12, 0, 0, DateTimeKind.Unspecified), "Quality Assurance", "Dewi Lestari", new DateTime(2026, 3, 5, 14, 30, 15, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 5, 1, new DateTime(2026, 3, 5, 14, 30, 15, 0, DateTimeKind.Unspecified), "dewi89" },
                    { 14, "CAT0013", new DateTime(2026, 3, 2, 11, 20, 0, 0, DateTimeKind.Unspecified), "Production", "Eko Nugroho", new DateTime(2026, 3, 5, 8, 45, 10, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 2, 1, new DateTime(2026, 3, 5, 8, 45, 10, 0, DateTimeKind.Unspecified), "eko25" },
                    { 15, "CAT0014", new DateTime(2026, 3, 2, 13, 30, 0, 0, DateTimeKind.Unspecified), "Maintenance", "Siti Aminah", new DateTime(2026, 3, 4, 16, 10, 0, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 4, 1, new DateTime(2026, 3, 4, 16, 10, 0, 0, DateTimeKind.Unspecified), "siti64" },
                    { 16, "CAT0015", new DateTime(2026, 3, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), "Production", "Joko Susilo", new DateTime(2026, 3, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 2, 3, new DateTime(2026, 3, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), "joko11" },
                    { 17, "CAT0016", new DateTime(2026, 3, 4, 15, 45, 0, 0, DateTimeKind.Unspecified), "Production", "Ratna Sari", new DateTime(2026, 3, 3, 13, 15, 20, 0, DateTimeKind.Unspecified), "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm", 6, 1, new DateTime(2026, 3, 3, 13, 15, 20, 0, DateTimeKind.Unspecified), "ratna97" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestConfigNets_ConfigurationConfigId",
                table: "TestConfigNets",
                column: "ConfigurationConfigId");

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
                name: "MaintenanceStates");

            migrationBuilder.DropTable(
                name: "SystemLogs");

            migrationBuilder.DropTable(
                name: "TestConfigNets");

            migrationBuilder.DropTable(
                name: "TestDetails");

            migrationBuilder.DropTable(
                name: "TestSteps");

            migrationBuilder.DropTable(
                name: "Users");

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
