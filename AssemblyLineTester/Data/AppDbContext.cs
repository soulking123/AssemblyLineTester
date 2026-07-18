using Microsoft.EntityFrameworkCore;
using AssemblyLineTester.Models;
using System;

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

        /// Configures the database provider and connection string.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string appDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AssemblyLineTester");

            Directory.CreateDirectory(appDataFolder); // create if not exists

            string dbPath = Path.Combine(appDataFolder, "users.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        /// Configures entity mapping relationships, unique indexes, and seeds initial data.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Configure Unique Indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<UserStatus>()
                .HasIndex(s => s.Name)
                .IsUnique();

            // 2. Configure relationship cascade delete rules for Roles (Restrict delete to prevent orphan records)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. Configure relationship cascade delete rules for Statuses (Restrict delete to prevent orphan records)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Status)
                .WithMany()
                .HasForeignKey(u => u.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // 4. Seed initial Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrator", Description = "Full system administration access" },
                new Role { Id = 2, Name = "Operator", Description = "Standard machine line operations" },
                new Role { Id = 3, Name = "Engineer", Description = "System configuration and engineering access" },
                new Role { Id = 4, Name = "Technician", Description = "Hardware maintenance and troubleshooting access" },
                new Role { Id = 5, Name = "Quality", Description = "Quality assurance inspection access" },
                new Role { Id = 6, Name = "Supervisor", Description = "Line shift supervision access" }
            );

            // 5. Seed initial User Statuses
            modelBuilder.Entity<UserStatus>().HasData(
                new UserStatus { Id = 1, Name = "Active", Description = "User is active and allowed to log in" },
                new UserStatus { Id = 2, Name = "Inactive", Description = "User is inactive and disabled" },
                new UserStatus { Id = 3, Name = "Locked", Description = "User is locked out due to multiple failed login attempts" }
            );

            // 6. Seed initial users with secure BCrypt hashes
            const string passHash = "$2b$11$oiAF9zslwUXNz1Zr1O6MDOtiiD1wGnPvR2bNJ6hekI2L1mhz8ErGm";
            const string faizHash = "$2b$11$rZFf4GHizQh2xGlxxdSj4eRzsOf4gSoVdwbLOcvwfozyn5nVfKd/2";

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    PasswordHash = passHash,
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
                    Username = "fally12",
                    PasswordHash = passHash,
                    FullName = "Fally Kusuma",
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
                    Username = "faiz",
                    PasswordHash = faizHash,
                    FullName = "Faiz",
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
                    Username = "doni42",
                    PasswordHash = passHash,
                    FullName = "Doni Pratama",
                    CompanyId = "CAT0003",
                    RoleId = 2, // Operator
                    Department = "Production",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-12T14:10:05"),
                    LastLogin = DateTime.Parse("2026-03-15T09:55:33"),
                    UpdatedAt = DateTime.Parse("2026-03-15T09:55:33")
                },
                new User
                {
                    UserId = 5,
                    Username = "rian24",
                    PasswordHash = passHash,
                    FullName = "Rian Hidayat",
                    CompanyId = "CAT0004",
                    RoleId = 4, // Technician
                    Department = "Maintenance",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-12T15:45:30"),
                    LastLogin = DateTime.Parse("2026-03-14T16:22:18"),
                    UpdatedAt = DateTime.Parse("2026-03-14T16:22:18")
                },
                new User
                {
                    UserId = 6,
                    Username = "maria57",
                    PasswordHash = passHash,
                    FullName = "Maria Sari",
                    CompanyId = "CAT0005",
                    RoleId = 2, // Operator
                    Department = "Production",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-13T09:02:44"),
                    LastLogin = DateTime.Parse("2026-03-15T08:17:09"),
                    UpdatedAt = DateTime.Parse("2026-03-15T08:17:09")
                },
                new User
                {
                    UserId = 7,
                    Username = "agus08",
                    PasswordHash = passHash,
                    FullName = "Agus Setiawan",
                    CompanyId = "CAT0006",
                    RoleId = 3, // Engineer
                    Department = "Engineering",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-13T11:21:36"),
                    LastLogin = DateTime.Parse("2026-03-15T07:58:23"),
                    UpdatedAt = DateTime.Parse("2026-03-15T07:58:23")
                },
                new User
                {
                    UserId = 8,
                    Username = "lina15",
                    PasswordHash = passHash,
                    FullName = "Lina Putri",
                    CompanyId = "CAT0007",
                    RoleId = 5, // Quality
                    Department = "Quality Assurance",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-14T10:05:17"),
                    LastLogin = DateTime.Parse("2026-03-14T17:11:05"),
                    UpdatedAt = DateTime.Parse("2026-03-14T17:11:05")
                },
                new User
                {
                    UserId = 9,
                    Username = "yudi89",
                    PasswordHash = passHash,
                    FullName = "Yudi Rahman",
                    CompanyId = "CAT0008",
                    RoleId = 4, // Technician
                    Department = "Maintenance",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-14T13:17:08"),
                    LastLogin = DateTime.Parse("2026-03-14T15:33:44"),
                    UpdatedAt = DateTime.Parse("2026-03-14T15:33:44")
                },
                new User
                {
                    UserId = 10,
                    Username = "budi24",
                    PasswordHash = passHash,
                    FullName = "Budi Santoso",
                    CompanyId = "CAT0009",
                    RoleId = 6, // Supervisor
                    Department = "Production",
                    StatusId = 2, // Inactive
                    CreatedAt = DateTime.Parse("2026-03-05T09:40:10"),
                    LastLogin = DateTime.Parse("2026-03-08T14:12:33"),
                    UpdatedAt = DateTime.Parse("2026-03-08T14:12:33")
                },
                new User
                {
                    UserId = 11,
                    Username = "lisa11",
                    PasswordHash = passHash,
                    FullName = "Lisa Kartika",
                    CompanyId = "CAT0010",
                    RoleId = 2, // Operator
                    Department = "Production",
                    StatusId = 2, // Inactive
                    CreatedAt = DateTime.Parse("2026-03-06T14:56:31"),
                    LastLogin = DateTime.Parse("2026-03-07T11:07:15"),
                    UpdatedAt = DateTime.Parse("2026-03-07T11:07:15")
                },
                new User
                {
                    UserId = 12,
                    Username = "hendra655",
                    PasswordHash = passHash,
                    FullName = "Hendra Wijaya",
                    CompanyId = "CAT0011",
                    RoleId = 3, // Engineer
                    Department = "Engineering",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-01T08:15:00"),
                    LastLogin = DateTime.Parse("2026-03-06T10:15:00"),
                    UpdatedAt = DateTime.Parse("2026-03-06T10:15:00")
                },
                new User
                {
                    UserId = 13,
                    Username = "dewi89",
                    PasswordHash = passHash,
                    FullName = "Dewi Lestari",
                    CompanyId = "CAT0012",
                    RoleId = 5, // Quality
                    Department = "Quality Assurance",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-01T09:12:00"),
                    LastLogin = DateTime.Parse("2026-03-05T14:30:15"),
                    UpdatedAt = DateTime.Parse("2026-03-05T14:30:15")
                },
                new User
                {
                    UserId = 14,
                    Username = "eko25",
                    PasswordHash = passHash,
                    FullName = "Eko Nugroho",
                    CompanyId = "CAT0013",
                    RoleId = 2, // Operator
                    Department = "Production",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-02T11:20:00"),
                    LastLogin = DateTime.Parse("2026-03-05T08:45:10"),
                    UpdatedAt = DateTime.Parse("2026-03-05T08:45:10")
                },
                new User
                {
                    UserId = 15,
                    Username = "siti64",
                    PasswordHash = passHash,
                    FullName = "Siti Aminah",
                    CompanyId = "CAT0014",
                    RoleId = 4, // Technician
                    Department = "Maintenance",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-02T13:30:00"),
                    LastLogin = DateTime.Parse("2026-03-04T16:10:00"),
                    UpdatedAt = DateTime.Parse("2026-03-04T16:10:00")
                },
                new User
                {
                    UserId = 16,
                    Username = "joko11",
                    PasswordHash = passHash,
                    FullName = "Joko Susilo",
                    CompanyId = "CAT0015",
                    RoleId = 2, // Operator
                    Department = "Production",
                    StatusId = 3, // Locked
                    CreatedAt = DateTime.Parse("2026-03-03T10:00:00"),
                    LastLogin = DateTime.Parse("2026-03-03T09:00:00"),
                    UpdatedAt = DateTime.Parse("2026-03-03T09:00:00")
                },
                new User
                {
                    UserId = 17,
                    Username = "ratna97",
                    PasswordHash = passHash,
                    FullName = "Ratna Sari",
                    CompanyId = "CAT0016",
                    RoleId = 6, // Supervisor
                    Department = "Production",
                    StatusId = 1, // Active
                    CreatedAt = DateTime.Parse("2026-03-04T15:45:00"),
                    LastLogin = DateTime.Parse("2026-03-03T13:15:20"),
                    UpdatedAt = DateTime.Parse("2026-03-03T13:15:20")
                }
            );

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
                    LastUsageDate = DateTime.Today,
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