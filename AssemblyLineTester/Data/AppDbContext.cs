using AssemblyLineTester.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace AssemblyLineTester.Data
{
    /// Main database context for Entity Framework Core.
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

        /// Configures the database provider and connection string.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string appDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AssemblyLineTester");

            Directory.CreateDirectory(appDataFolder); // create if not exists

            string dbPath = Path.Combine(appDataFolder, "users.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
            optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        /// Configures entity mapping relationships, unique indexes, and seeds initial data.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Unique Indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<UserStatus>()
                .HasIndex(s => s.Name)
                .IsUnique();

            // Configure relationship cascade delete rules for Roles (Restrict delete to prevent orphan records)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relationship cascade delete rules for Statuses (Restrict delete to prevent orphan records)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Status)
                .WithMany()
                .HasForeignKey(u => u.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique indexes for the new tables
            modelBuilder.Entity<TestReport>()
                .HasIndex(r => r.ReportNumber)
                .IsUnique();

            modelBuilder.Entity<TestType>()
                .HasIndex(t => t.TestTypeName)
                .IsUnique();

            // TestDetails -> TestReports (cascade delete: deleting a report removes its details)
            modelBuilder.Entity<TestDetail>()
                .HasOne(d => d.Report)
                .WithMany(r => r.Details)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.Cascade);

            // TestDetails -> TestTypes (lookup by name, restrict delete to prevent orphan details)
            modelBuilder.Entity<TestDetail>()
                .HasOne(d => d.TestTypeInfo)
                .WithMany()
                .HasForeignKey(d => d.TestTypeName)
                .HasPrincipalKey(t => t.TestTypeName);

            // Configure Report → TestStep relationship (One Report has many Test Steps)
            modelBuilder.Entity<TestStep>()
                .HasOne(s => s.Report)
                .WithMany(r => r.Steps)
                .HasForeignKey(s => s.ReportId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure TestTypeInfo → TestStep relationship using TestTypeName
            modelBuilder.Entity<TestStep>()
                .HasOne(s => s.TestTypeInfo)
                .WithMany()
                .HasForeignKey(s => s.TestTypeName)
                .HasPrincipalKey(t => t.TestTypeName)
                .OnDelete(DeleteBehavior.Restrict);

            // Ensure StepNo is unique within each Report
            modelBuilder.Entity<TestStep>()
                .HasIndex(s => new { s.ReportId, s.StepNo })
                .IsUnique();

            // Speeds up Test History pagination/sorting once row count grows into the hundreds/thousands.
            modelBuilder.Entity<TestReport>()
                .HasIndex(r => r.TestDateTime);

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

            // Configure RolePermission entity and unique index
            modelBuilder.Entity<RolePermission>()
                .HasIndex(rp => new { rp.RoleId, rp.PermissionKey })
                .IsUnique();

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            //TestReport → ReportTestTypeSummary relationship (One Report has many Test Type Summaries)
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

            // Speeds up the Dashboard's date-range + fail-breakdown query.
            modelBuilder.Entity<ReportTestTypeSummary>()
                .HasIndex(s => s.TestTypeName);

            // Connector entity configuration
            modelBuilder.Entity<Connector>()
                .HasOne(c => c.ProductHarness)
                .WithMany(p => p.ConnectorList)
                .HasForeignKey(c => c.ProductHarnessId)
                .OnDelete(DeleteBehavior.Cascade);

            // Save ImgePath as unique for each ProductHarness
            string imagesFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AssemblyLineTester", "Images");

            //======== SEED DATA BELOW =========//

            // Seed initial Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrator", Description = "Full system administration access" },
                new Role { Id = 2, Name = "Operator", Description = "Standard machine line operations" },
                new Role { Id = 3, Name = "Engineer", Description = "System configuration and engineering access" },
                new Role { Id = 4, Name = "Technician", Description = "Hardware maintenance and troubleshooting access" },
                new Role { Id = 5, Name = "Quality", Description = "Quality assurance inspection access" },
                new Role { Id = 6, Name = "Supervisor", Description = "Line shift supervision access" }
            );

            // Seed initial User Statuses
            modelBuilder.Entity<UserStatus>().HasData(
                new UserStatus { Id = 1, Name = "Active", Description = "User is active and allowed to log in" },
                new UserStatus { Id = 2, Name = "Inactive", Description = "User is inactive and disabled" },
                new UserStatus { Id = 3, Name = "Locked", Description = "User is locked out due to multiple failed login attempts" }
            );

            // Seed initial users with secure BCrypt hashes
            const string adminHash = "$2b$11$yGJN27FWp/BWcUo0M4ZOW.mWsQGCKQwQfVoemKJyPF2MFLP9ickMS";
            const string faizHash = "$2b$11$VAO3CvR8UbFBe9Pn2sNrG.1Wg5vA7FpwVqPyxQeME/5PGTQ4PYk2u";
            const string tegarHash = "$2b$11$SsUZzVTYh87OPBYbFMYNO.JNJ44jLe3wBxLq2cg7WFPWl.yHAIi6O";
            const string khenHash = "$2b$11$JzVT2cjdgorN0AVlQHKv5uNCt1fNbQvlW7J1nEqd0pk2hDCAihDPe";

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    PasswordHash = adminHash,
                    FullName = "System Administrator",
                    CompanyId = "CAT0100",
                    RoleId = 1, // Administrator
                    Department = "Engineering",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-01T08:00:00Z"),
                    UpdatedAt = DateTime.Parse("2026-03-01T08:00:00Z")
                },
                new User
                {
                    UserId = 2,
                    Username = "faiz",
                    PasswordHash = faizHash,
                    FullName = "Faiz",
                    CompanyId = "CAT0001",
                    RoleId = 1, // Administrator
                    Department = "Engineering",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-10T09:15:22"),
                    LastLogin = DateTime.Parse("2026-03-15T10:28:41"),
                    UpdatedAt = DateTime.Parse("2026-03-15T10:28:41")
                },
                new User
                {
                    UserId = 3,
                    Username = "khen",
                    PasswordHash = khenHash,
                    FullName = "Khen",
                    CompanyId = "CAT0002",
                    RoleId = 3, // Engineer
                    Department = "Engineering",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-11T08:23:15"),
                    LastLogin = DateTime.Parse("2026-03-15T09:41:12"),
                    UpdatedAt = DateTime.Parse("2026-03-15T09:41:12")
                },
                new User
                {
                    UserId = 4,
                    Username = "tegar",
                    PasswordHash = tegarHash,
                    FullName = "Tegar",
                    CompanyId = "CAT0003",
                    RoleId = 2, // Operator
                    Department = "Production",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-12T14:10:05"),
                    LastLogin = DateTime.Parse("2026-03-15T09:55:33"),
                    UpdatedAt = DateTime.Parse("2026-03-15T09:55:33")
                }
            );

            // Seed initial test types
            modelBuilder.Entity<TestType>().HasData(
                new TestType { Id = 1, TestTypeName = "4RESISTOR", DisplayName = "4RESISTOR" },
                new TestType { Id = 2, TestTypeName = "4WIRE", DisplayName = "4WIRE" },
                new TestType { Id = 3, TestTypeName = "CAPACITOR", DisplayName = "CAPACITOR" },
                new TestType { Id = 4, TestTypeName = "CLOSE", DisplayName = "CLOSE" },
                new TestType { Id = 5, TestTypeName = "COMMENT", DisplayName = "COMMENT" },
                new TestType { Id = 6, TestTypeName = "DIODE", DisplayName = "DIODE" },
                new TestType { Id = 7, TestTypeName = "LINK", DisplayName = "LINK" },
                new TestType { Id = 8, TestTypeName = "LUA COMPONENT", DisplayName = "LUA COMPONENT" },
                new TestType { Id = 9, TestTypeName = "OPEN", DisplayName = "OPEN" },
                new TestType { Id = 10, TestTypeName = "PROBE", DisplayName = "PROBE" },
                new TestType { Id = 11, TestTypeName = "RESISTOR", DisplayName = "RESISTOR" },
                new TestType { Id = 12, TestTypeName = "SIGNATURE DIODE", DisplayName = "SIGNATURE DIODE" },
                new TestType { Id = 13, TestTypeName = "SIGNATURE WIRE", DisplayName = "SIGNATURE WIRE" },
                new TestType { Id = 14, TestTypeName = "TWISTED PAIR", DisplayName = "TWISTED PAIR" },
                new TestType { Id = 15, TestTypeName = "WIRE", DisplayName = "WIRE" }
            );

            // Seed initial Product Harnesses
            modelBuilder.Entity<ProductHarness>().HasData(
                new ProductHarness
                {
                    ProductHarnessId = 1,
                    Name = "Main Engine Harness 6020",
                    ProductModel = "6020",
                    ProductType = "AC",
                    Area = "Engine Control",
                    LastUpdate = DateTime.Parse("2026-06-01T08:00:00Z"),
                    ImagePath = @"Assets/img/Harness/6040.png",
                    ConnectorListName = new List<string> { "J1", "J2" }
                },
                new ProductHarness
                {
                    ProductHarnessId = 2,
                    Name = "Cabin Interior Harness 6030",
                    ProductModel = "6030",
                    ProductType = "AC",
                    Area = "Cabin Interior",
                    LastUpdate = DateTime.Parse("2026-06-01T08:00:00Z"),
                    ConnectorListName = new List<string> { "J1" }
                },
                new ProductHarness
                {
                    ProductHarnessId = 3,
                    Name = "Chassis Harness 6040",
                    ProductModel = "6040",
                    ProductType = "AC",
                    Area = "Chassis",
                    LastUpdate = DateTime.Parse("2026-06-01T08:00:00Z"),
                    ImagePath = @"Assets/img/Harness/6040.png",
                    ConnectorListName = new List<string> { "J1", "J2", "J3" }
                },
                new ProductHarness
                {
                    ProductHarnessId = 4,
                    Name = "Dashboard Harness 6060",
                    ProductModel = "6060",
                    ProductType = "AC",
                    Area = "Dashboard",
                    LastUpdate = DateTime.Parse("2026-06-01T08:00:00Z"),
                    ConnectorListName = new List<string> { "J1" }
                }
            );

            // Seed initial Connectors 
            modelBuilder.Entity<Connector>().HasData(
                new Connector { ConnectorId = 1, Perf = "Standard", PartNumber = "D507F1", ContactType = "Female", ContactQuantity = 64, WireSize = "18 AWG", CurrentRating = 5, MountingStyle = "Panel Mount", ImagePath = @"Assets/img/Connector/J1.png", ProductHarnessId = 1 },
                new Connector { ConnectorId = 2, Perf = "Standard", PartNumber = "D507F2", ContactType = "Male", ContactQuantity = 64, WireSize = "18 AWG", CurrentRating = 5, MountingStyle = "Panel Mount", ProductHarnessId = 1 },
                new Connector { ConnectorId = 3, Perf = "Standard", PartNumber = "D507F1", ContactType = "Female", ContactQuantity = 32, WireSize = "20 AWG", CurrentRating = 3, MountingStyle = "Free Hanging", ImagePath = @"Assets/img/Connector/J1.png", ProductHarnessId = 2 },
                new Connector { ConnectorId = 4, Perf = "Standard", PartNumber = "D507F1", ContactType = "Female", ContactQuantity = 64, WireSize = "18 AWG", CurrentRating = 5, MountingStyle = "Panel Mount", ImagePath = @"Assets/img/Connector/J1.png", ProductHarnessId = 3 },
                new Connector { ConnectorId = 5, Perf = "Standard", PartNumber = "D507F2", ContactType = "Male", ContactQuantity = 64, WireSize = "18 AWG", CurrentRating = 5, MountingStyle = "Panel Mount", ProductHarnessId = 3 },
                new Connector { ConnectorId = 6, Perf = "Standard", PartNumber = "D507F3", ContactType = "Female", ContactQuantity = 16, WireSize = "22 AWG", CurrentRating = 2, MountingStyle = "Panel Mount", ProductHarnessId = 3 },
                new Connector { ConnectorId = 7, Perf = "Standard", PartNumber = "D507F1", ContactType = "Female", ContactQuantity = 64, WireSize = "18 AWG", CurrentRating = 5, MountingStyle = "Panel Mount", ImagePath = @"Assets/img/Connector/J1.png", ProductHarnessId = 4 }
            );


            modelBuilder.Entity<RolePermission>().HasData(GetInitialRolePermissions());
        }

        private static RolePermission[] GetInitialRolePermissions()
        {
            var defs = new[]
            {
                ("Page.Dashboard", "Page Access", "Access Dashboard Page"),
                ("Page.TestExecution", "Page Access", "Access Test Execution Page"),
                ("Page.HarnessDatabase", "Page Access", "Access Harness Database Page"),
                ("Page.TestHistory", "Page Access", "Access Test History Page"),
                ("Page.Reports", "Page Access", "Access Reports Page"),
                ("Page.SystemLog", "Page Access", "Access System Log Page"),
                ("Page.Settings", "Page Access", "Access Settings Page"),
                ("Page.UserManagement", "Page Access", "Access User Management Page"),
                ("Action.TestExecution.Run", "Test Operations", "Start and Run Testing Operations"),
                ("Action.TestConfig.Edit", "Test Operations", "Add and Edit Test Parameters & Harness Specs"),
                ("Action.Instruments.Calibrate", "Test Operations", "Calibrate and Configure Hardware Instruments"),
                ("Action.UserManagement.Edit", "System & Logs", "Add, Edit, and Manage System User Accounts"),
                ("Action.Reports.Export", "System & Logs", "Export and Print Test Reports & Analytics"),
                ("Action.SystemLog.Clear", "System & Logs", "Clear and Purge System Activity Logs"),
                ("Action.Database.Backup", "System & Logs", "Create and Restore Database Backups")
            };

            var list = new List<RolePermission>();
            int idCounter = 1;

            for (int roleId = 1; roleId <= 6; roleId++)
            {
                foreach (var (key, group, desc) in defs)
                {
                    bool granted = false;
                    if (roleId == 1) // Administrator
                    {
                        granted = true;
                    }
                    else if (roleId == 2) // Operator
                    {
                        if (key is "Page.Dashboard" or "Page.TestExecution" or "Page.TestHistory" or "Action.TestExecution.Run")
                            granted = true;
                    }
                    else if (roleId == 3) // Engineer
                    {
                        if (key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.Reports" or "Page.SystemLog" or "Page.Settings" or "Action.TestExecution.Run" or "Action.TestConfig.Edit" or "Action.Instruments.Calibrate" or "Action.Reports.Export")
                            granted = true;
                    }
                    else if (roleId == 4) // Technician
                    {
                        if (key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.SystemLog" or "Page.Settings" or "Action.TestExecution.Run" or "Action.Instruments.Calibrate" or "Action.Reports.Export")
                            granted = true;
                    }
                    else if (roleId == 5) // Quality
                    {
                        if (key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.Reports" or "Action.Reports.Export")
                            granted = true;
                    }
                    else if (roleId == 6) // Supervisor
                    {
                        if (key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.Reports" or "Page.SystemLog" or "Action.TestExecution.Run" or "Action.Reports.Export")
                            granted = true;
                    }

                    list.Add(new RolePermission
                    {
                        Id = idCounter++,
                        RoleId = roleId,
                        PermissionKey = key,
                        PermissionGroup = group,
                        Description = desc,
                        IsGranted = granted
                    });
                }
            }

            return list.ToArray();
        }
    }
}