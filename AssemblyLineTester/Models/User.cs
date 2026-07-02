using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssemblyLineTester.Models
{
    /// Represents a system user account in the application.
    public class User
    {
        /// Unique identifier for the User.
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string CompanyId { get; set; } = string.Empty;

        /// Foreign Key referring to the associated Role and navigation property for the assigned Role.
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;

        public string Department { get; set; } = string.Empty;

        /// Foreign Key referring to the associated UserStatus and navigation property for the assigned UserStatus.
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public UserStatus Status { get; set; } = null!;

        public DateTime? LastLogin { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
