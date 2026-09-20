using AssemblyLineTester.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace AssemblyLineTester.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<RolePermission> RolePermissions { get; set; } = null!;
        public DbSet<UserStatus> UserStatuses { get; set; } = null!;
        public DbSet<MaintenanceState> MaintenanceStates { get; set; } = null!;
        public DbSet<SystemLog> SystemLogs { get; set; } = null!;
        public DbSet<TestReport> TestReports { get; set; } = null!;
        public DbSet<TestDetail> TestDetails { get; set; } = null!;
        public DbSet<TestType> TestTypes { get; set; } = null!;
        public DbSet<TestStep> TestSteps { get; set; } = null!;
        public DbSet<TestConfiguration> TestConfigurations { get; set; } = null!;
        public DbSet<TestConfigNet> TestConfigNets { get; set; } = null!;
        public DbSet<ReportTestTypeSummary> ReportTestTypeSummaries { get; set; } = null!;
        public DbSet<ProductHarness> ProductHarnesses { get; set; } = null!;
        public DbSet<Connector> Connectors { get; set; } = null!;
        public DbSet<HarnessConnector> HarnessConnectors { get; set; } = null!;
        public DbSet<DefaultTestSetting> DefaultTestSettings { get; set; } = null!;
        public DbSet<LearnConnection> LearnConnections { get; set; } = null!;
        public DbSet<TestResultCheck> TestResultChecks { get; set; } = null!;
        public DbSet<TestTypeCheck> TestTypeChecks { get; set; } = null!;
        public DbSet<ProductModel> ProductModels { get; set; } = null!;
        public DbSet<Area> Areas { get; set; } = null!;

        // Folder holding the live database, its backups and the pre-restore copies.
        public static string DataFolder { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AssemblyLineTester");

        // The single definition of the live database path.
        public static string DbPath { get; } = Path.Combine(DataFolder, "users.db");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Directory.CreateDirectory(DataFolder);

            optionsBuilder.UseSqlite($"Data Source={DbPath}");
            // PendingModelChangesWarning stays unsuppressed; it is the only signal of model drift.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Filtered, so the many accounts without a Company ID do not collide.
            modelBuilder.Entity<User>()
                .HasIndex(u => u.CompanyId)
                .IsUnique()
                .HasFilter("\"CompanyId\" <> ''");

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<UserStatus>()
                .HasIndex(s => s.Name)
                .IsUnique();

            // Restrict, so deleting a role cannot orphan its users.
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Status)
                .WithMany()
                .HasForeignKey(u => u.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TestReport>()
                .HasIndex(r => r.ReportNumber)
                .IsUnique();

            modelBuilder.Entity<TestType>()
                .HasIndex(t => t.TestTypeName)
                .IsUnique();

            // Deleting a report takes its details with it.
            modelBuilder.Entity<TestDetail>()
                .HasOne(d => d.Report)
                .WithMany(r => r.Details)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TestDetail>()
                .HasOne(d => d.TestTypeInfo)
                .WithMany()
                .HasForeignKey(d => d.TestTypeName)
                .HasPrincipalKey(t => t.TestTypeName);

            modelBuilder.Entity<TestStep>()
                .HasOne(s => s.Report)
                .WithMany(r => r.Steps)
                .HasForeignKey(s => s.ReportId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TestStep>()
                .HasOne(s => s.TestTypeInfo)
                .WithMany()
                .HasForeignKey(s => s.TestTypeName)
                .HasPrincipalKey(t => t.TestTypeName)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TestStep>()
                .HasIndex(s => new { s.ReportId, s.StepNo })
                .IsUnique();

            // Speeds up Test History pagination once the row count grows.
            modelBuilder.Entity<TestReport>()
                .HasIndex(r => r.TestDateTime);

            // Every System Log query filters and sorts on Timestamp; six months is a lot to scan.
            modelBuilder.Entity<SystemLog>()
                .HasIndex(l => l.Timestamp);

            // Named explicitly, or EF invents a shadow FK column and leaves ConfigId at zero.
            modelBuilder.Entity<TestConfigNet>()
                .HasOne(n => n.Configuration)
                .WithMany(c => c.Nets)
                .HasForeignKey(n => n.ConfigId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MaintenanceState>().HasData(
                new MaintenanceState
                {
                    Id = 1,
                    LastUsageDate = DateTime.Parse("2026-01-01"),
                    OperatingSecondsToday = 0,
                    RemainingMaintDays = 180,
                    RemainingMaintHours = 2160.0
                }
            );

            modelBuilder.Entity<RolePermission>()
                .HasIndex(rp => new { rp.RoleId, rp.PermissionKey })
                .IsUnique();

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReportTestTypeSummary>()
                .HasOne(s => s.Report)
                .WithMany()
                .HasForeignKey(s => s.ReportId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReportTestTypeSummary>()
                .HasOne(s => s.TestTypeInfo)
                .WithMany()
                .HasForeignKey(s => s.TestTypeName)
                .HasPrincipalKey(t => t.TestTypeName)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReportTestTypeSummary>()
                .HasIndex(s => new { s.ReportId, s.TestTypeName })
                .IsUnique();

            // Speeds up the Dashboard fail-breakdown query.
            modelBuilder.Entity<ReportTestTypeSummary>()
                .HasIndex(s => s.TestTypeName);

            // Join table for the many-to-many between harnesses and connectors.
            modelBuilder.Entity<HarnessConnector>()
                .HasOne(hc => hc.ProductHarness)
                .WithMany(p => p.HarnessConnectors)
                .HasForeignKey(hc => hc.ProductHarnessId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HarnessConnector>()
                .HasOne(hc => hc.Connector)
                .WithMany()
                .HasForeignKey(hc => hc.ConnectorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HarnessConnector>()
                .HasIndex(hc => new { hc.ProductHarnessId, hc.ConnectorId })
                .IsUnique();

            // Restrict, so deleting a test type cannot orphan default settings or learn connections that reference it.
            modelBuilder.Entity<DefaultTestSetting>()
                .HasOne(s => s.DefaultTestType)
                .WithMany()
                .HasForeignKey(s => s.DefaultTestTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LearnConnection>()
                .HasOne(c => c.TestType)
                .WithMany()
                .HasForeignKey(c => c.TestTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductModel>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Area>().HasIndex(a => a.Name).IsUnique();

            // TestType carries no inverse collection, so the join table is left implicit.
            modelBuilder.Entity<TestConfiguration>()
                .HasMany(c => c.TestTypes)
                .WithMany();

            // ---- Seed data ----

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrator", Description = "Full system administration access" },
                new Role { Id = 2, Name = "Operator", Description = "Standard machine line operations" },
                new Role { Id = 3, Name = "Engineer", Description = "System configuration and engineering access" },
                new Role { Id = 4, Name = "Technician", Description = "Hardware maintenance and troubleshooting access" },
                new Role { Id = 5, Name = "Quality", Description = "Quality assurance inspection access" },
                new Role { Id = 6, Name = "Supervisor", Description = "Line shift supervision access" }
            );

            modelBuilder.Entity<UserStatus>().HasData(
                new UserStatus { Id = 1, Name = "Active", Description = "User is active and allowed to log in" },
                new UserStatus { Id = 2, Name = "Inactive", Description = "User is inactive and disabled" }
            );

            // Same starting password as UserService.DefaultNewUserPassword ("Cat12345")
            const string adminHash = "$2a$11$Hcpu0fJL6xvHgmbYJNv/s.QMrlL8F9ALRHip20cUQLY6OSro3YIhC";

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    PasswordHash = adminHash,
                    FullName = "System Administrator",
                    CompanyId = "CAT0100",
                    RoleId = 1,
                    Department = "Engineering",
                    StatusId = 1,
                    MustChangePassword = true,
                    CreatedAt = DateTime.Parse("2026-03-01T08:00:00Z"),
                    UpdatedAt = DateTime.Parse("2026-03-01T08:00:00Z")
                }
            );

            modelBuilder.Entity<TestType>().HasData(
                new TestType { Id = 1, TestTypeName = "4RESISTOR", DisplayName = "4RESISTOR" },
                new TestType { Id = 2, TestTypeName = "4WIRE", DisplayName = "4WIRE" },
                new TestType { Id = 3, TestTypeName = "CAPACITOR", DisplayName = "CAPACITOR", TestTypeValue = 2 },
                new TestType { Id = 4, TestTypeName = "CLOSE", DisplayName = "CLOSE" },
                new TestType { Id = 5, TestTypeName = "COMMENT", DisplayName = "COMMENT" },
                new TestType { Id = 6, TestTypeName = "DIODE", DisplayName = "DIODE", TestTypeValue = 4 },
                new TestType { Id = 7, TestTypeName = "LINK", DisplayName = "LINK" },
                new TestType { Id = 8, TestTypeName = "LUA COMPONENT", DisplayName = "LUA COMPONENT" },
                new TestType { Id = 9, TestTypeName = "OPEN", DisplayName = "OPEN" },
                new TestType { Id = 10, TestTypeName = "PROBE", DisplayName = "PROBE" },
                new TestType { Id = 11, TestTypeName = "RESISTOR", DisplayName = "RESISTOR", TestTypeValue = 1 },
                new TestType { Id = 12, TestTypeName = "SIGNATURE DIODE", DisplayName = "SIGNATURE DIODE" },
                new TestType { Id = 13, TestTypeName = "SIGNATURE WIRE", DisplayName = "SIGNATURE WIRE" },
                new TestType { Id = 14, TestTypeName = "TWISTED PAIR", DisplayName = "TWISTED PAIR", TestTypeValue = 8 },
                new TestType { Id = 15, TestTypeName = "WIRE", DisplayName = "WIRE", TestTypeValue = 0 }
            );

            // One check row per test type, so the Settings list is complete the first time it opens.
            modelBuilder.Entity<TestTypeCheck>().HasData(
                new TestTypeCheck { Id = 1, TestTypeName = "4RESISTOR", DisplayName = "4RESISTOR", IsChecked = false },
                new TestTypeCheck { Id = 2, TestTypeName = "4WIRE", DisplayName = "4WIRE", IsChecked = false },
                new TestTypeCheck { Id = 3, TestTypeName = "CAPACITOR", DisplayName = "CAPACITOR", IsChecked = false },
                new TestTypeCheck { Id = 4, TestTypeName = "CLOSE", DisplayName = "CLOSE", IsChecked = false },
                new TestTypeCheck { Id = 5, TestTypeName = "COMMENT", DisplayName = "COMMENT", IsChecked = false },
                new TestTypeCheck { Id = 6, TestTypeName = "DIODE", DisplayName = "DIODE", IsChecked = false },
                new TestTypeCheck { Id = 7, TestTypeName = "LINK", DisplayName = "LINK", IsChecked = false },
                new TestTypeCheck { Id = 8, TestTypeName = "LUA COMPONENT", DisplayName = "LUA COMPONENT", IsChecked = false },
                new TestTypeCheck { Id = 9, TestTypeName = "OPEN", DisplayName = "OPEN", IsChecked = false },
                new TestTypeCheck { Id = 10, TestTypeName = "PROBE", DisplayName = "PROBE", IsChecked = false },
                new TestTypeCheck { Id = 11, TestTypeName = "RESISTOR", DisplayName = "RESISTOR", IsChecked = true },
                new TestTypeCheck { Id = 12, TestTypeName = "SIGNATURE DIODE", DisplayName = "SIGNATURE DIODE", IsChecked = false },
                new TestTypeCheck { Id = 13, TestTypeName = "SIGNATURE WIRE", DisplayName = "SIGNATURE WIRE", IsChecked = true },
                new TestTypeCheck { Id = 14, TestTypeName = "TWISTED PAIR", DisplayName = "TWISTED PAIR", IsChecked = false },
                new TestTypeCheck { Id = 15, TestTypeName = "WIRE", DisplayName = "WIRE", IsChecked = true }
            );

            // The five types the Learn Connection modal offers, pointed at their TestType rows.
            modelBuilder.Entity<LearnConnection>().HasData(
                new LearnConnection { Id = 1, TestTypeId = 15, DisplayName = "Wire", IsChecked = true },
                new LearnConnection { Id = 2, TestTypeId = 11, DisplayName = "Resistance", IsChecked = true },
                new LearnConnection { Id = 3, TestTypeId = 3, DisplayName = "Capacitor", IsChecked = false },
                new LearnConnection { Id = 4, TestTypeId = 6, DisplayName = "Diode", IsChecked = false },
                new LearnConnection { Id = 5, TestTypeId = 14, DisplayName = "Twisted Pair", IsChecked = false }
            );

            // TestResultName doubles as the column header wherever a result row is rendered.
            modelBuilder.Entity<TestResultCheck>().HasData(
                new TestResultCheck { Id = 1, TestResultName = "Net-ID", IsChecked = true },
                new TestResultCheck { Id = 2, TestResultName = "Harness Group", IsChecked = true },
                new TestResultCheck { Id = 3, TestResultName = "From", IsChecked = true },
                new TestResultCheck { Id = 4, TestResultName = "Net From Label", IsChecked = true },
                new TestResultCheck { Id = 5, TestResultName = "To", IsChecked = true },
                new TestResultCheck { Id = 6, TestResultName = "Net To Label", IsChecked = true },
                new TestResultCheck { Id = 7, TestResultName = "Test Type", IsChecked = true },
                new TestResultCheck { Id = 8, TestResultName = "Lower Limit", IsChecked = true },
                new TestResultCheck { Id = 9, TestResultName = "Upper Limit", IsChecked = true },
                new TestResultCheck { Id = 10, TestResultName = "Expected Value", IsChecked = true },
                new TestResultCheck { Id = 11, TestResultName = "Tolerance", IsChecked = true },
                new TestResultCheck { Id = 12, TestResultName = "Measurement", IsChecked = true },

                // Appended, never slotted mid-list: seed ids follow position and would renumber.
                new TestResultCheck { Id = 13, TestResultName = "Color", IsChecked = true },
                new TestResultCheck { Id = 14, TestResultName = "Gauge", IsChecked = true }
            );

            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel { Id = 1, Name = "6020" },
                new ProductModel { Id = 2, Name = "6030" },
                new ProductModel { Id = 3, Name = "6040" },
                new ProductModel { Id = 4, Name = "6060" }
            );

            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, Name = "Riser Cabinet" }
            );

            modelBuilder.Entity<RolePermission>().HasData(GetInitialRolePermissions());

            modelBuilder.Entity<DefaultTestSetting>().HasData(GetInitialDefaultTestSetting());


        }

        private static DefaultTestSetting[] GetInitialDefaultTestSetting()
        {
            return
            [
                new DefaultTestSetting
                {
                    Id = 1,
                    TotalTestPointsAvailable = 448,
                    DefaultTestTypeId = 15,
                    DefaultMaxWireResistance = 10.0,
                    DefaultMinInulationResistance = 500000.0,
                    DefaultResistorValue = 100.0,
                    DefaultResistorTolerance = 10,
                    DefaultCapacitorValue = 0.000001,
                    DefaultCapacitorTolerance = 10
                }
            ];
        }

        private static RolePermission[] GetInitialRolePermissions()
        {
            var defs = Services.RolePermissionService.GetStandardPermissionDefinitions();
            var list = new List<RolePermission>();
            int idCounter = 1;

            for (int roleId = 1; roleId <= 6; roleId++)
            {
                foreach (var (key, group, desc) in defs)
                {
                    list.Add(new RolePermission
                    {
                        Id = idCounter++,
                        RoleId = roleId,
                        PermissionKey = key,
                        PermissionGroup = group,
                        Description = desc,
                        IsGranted = Services.RolePermissionService.GetDefaultGrantState(roleId, key)
                    });
                }
            }

            return list.ToArray();
        }
    }
}