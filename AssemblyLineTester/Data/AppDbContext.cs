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
        public DbSet<UserStatus> UserStatuses { get; set; } = null!;

        /// Configures the database provider and connection string.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=users.db");
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
            string adminHash = BCrypt.Net.BCrypt.HashPassword("000000");
            string faizHash = BCrypt.Net.BCrypt.HashPassword("12345678");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    PasswordHash = adminHash,
                    FullName = "System Administrator",
                    CompanyId = "CAT0001",
                    RoleId = 1, // Administrator
                    Department = "Engineering",
                    StatusId = 1, // Active status
                    CreatedAt = DateTime.Parse("2026-07-02T00:00:00Z"),
                    UpdatedAt = DateTime.Parse("2026-07-02T00:00:00Z")
                },
                new User
                {
                    UserId = 2,
                    Username = "faiz",
                    PasswordHash = faizHash,
                    FullName = "Faiz",
                    CompanyId = "CAT0002",
                    RoleId = 3, // Engineer
                    Department = "Engineering",
                    StatusId = 1, // Active status
                    CreatedAt = DateTime.Parse("2026-07-02T00:00:00Z"),
                    UpdatedAt = DateTime.Parse("2026-07-02T00:00:00Z")
                }
            );
        }
    }
}
