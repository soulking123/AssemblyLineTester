using System.ComponentModel.DataAnnotations;

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
    }
}
