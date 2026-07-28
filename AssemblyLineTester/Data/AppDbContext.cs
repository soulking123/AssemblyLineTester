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
        public DbSet<HarnessConnector> HarnessConnectors { get; set; } = null!;

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

            // HarnessConnector: join table for the many-to-many ProductHarness<->Connector relationship.
            // Deleting a harness only removes its links (connectors may still be used elsewhere);
            // deleting a connector removes it from every harness that used it.
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

            // One seeded account only. The developer accounts that used to sit here were personal
            // logins, not data every customer should receive, and their hashes are in the git
            // history forever.
            //
            // This hash is public by definition - it ships inside the installer and is identical on
            // every machine - so the account is created with MustChangePassword set. It is a way in
            // during commissioning, not a credential.
            const string adminHash = "$2b$11$yGJN27FWp/BWcUo0M4ZOW.mWsQGCKQwQfVoemKJyPF2MFLP9ickMS";

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
                    MustChangePassword = true,
                    CreatedAt = DateTime.Parse("2026-03-01T08:00:00Z"),
                    UpdatedAt = DateTime.Parse("2026-03-01T08:00:00Z")
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

            // Seed initial role permissions
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