using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace AssemblyLineTester.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string CompanyId { get; set; } = string.Empty;

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;

        public string Department { get; set; } = string.Empty;

        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public UserStatus Status { get; set; } = null!;

        // Blocks the account until the password is changed; set whenever someone else chose it.
        public bool MustChangePassword { get; set; }

        // Consecutive failures since the last success, cleared on sign-in and when a lock is applied.
        public int FailedLoginCount { get; set; }

        // A UTC instant rather than a flag, so the lock expires without anyone clearing it.
        public DateTime? LockoutUntil { get; set; }

        // Attempts allowed before the account locks, and how long it stays locked.
        public const int MaxFailedLogins = 5;
        public const int LockoutMinutes = 15;

        // The instant is passed in so the rule can be checked without waiting for it to expire.
        public bool IsLockedOut(DateTime utcNow) => LockoutUntil is DateTime until && until > utcNow;

        // Debug-only guard on the lockout gate.
        [Conditional("DEBUG")]
        public static void SelfCheck()
        {
            var now = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);

            Debug.Assert(!new User().IsLockedOut(now), "an account that has never been locked must sign in");
            Debug.Assert(new User { LockoutUntil = now.AddMinutes(1) }.IsLockedOut(now), "a lock in the future must hold");
            Debug.Assert(!new User { LockoutUntil = now.AddMinutes(-1) }.IsLockedOut(now), "an expired lock must release on its own");
            Debug.Assert(!new User { LockoutUntil = now }.IsLockedOut(now), "a lock must release at its own expiry instant, not after it");
        }

        public DateTime? LastLogin { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
