using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AssemblyLineTester.Models
{
    public class UserStatus
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Seeded ids, from the HasData block in AppDbContext.
        public const int Active = 1;
        public const int Inactive = 2;
        public const int Locked = 3;

        // An allow-list: a status added later stays refused until someone widens this deliberately.
        public static bool CanSignIn(int statusId) => statusId == Active;

        // Debug-only guard on the sign-in gate
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
