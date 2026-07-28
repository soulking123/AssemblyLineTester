using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AssemblyLineTester.Models
{
    /// Represents the status of a user account (e.g. Active, Inactive, Locked).
    public class UserStatus
    {
        /// Unique identifier for the User Status.
        [Key]
        public int Id { get; set; }


        public string Name { get; set; } = string.Empty;


        public string? Description { get; set; }

        /// <summary>Seeded ids, from the HasData block in AppDbContext.</summary>
        public const int Active = 1;
        public const int Inactive = 2;
        public const int Locked = 3;

        /// <summary>
        /// Whether an account in this status may open a session.
        /// </summary>
        /// <remarks>
        /// An allow-list on purpose. A status added to the table later is refused until someone
        /// deliberately widens this, rather than being let in by a check that only knew how to
        /// name the statuses that existed when it was written.
        /// </remarks>
        public static bool CanSignIn(int statusId) => statusId == Active;

        /// <summary>Debug-only guard on the sign-in gate. Cheap enough to run at every login.</summary>
        [Conditional("DEBUG")]
        public static void SelfCheck()
        {
            Debug.Assert(CanSignIn(Active), "an active account must be able to sign in");
            Debug.Assert(!CanSignIn(Inactive), "a disabled account must not sign in");
            Debug.Assert(!CanSignIn(Locked), "a locked account must not sign in");
            Debug.Assert(!CanSignIn(0), "an unset status must not sign in");
            Debug.Assert(!CanSignIn(99), "an unknown status must not sign in");
        }
    }
}
