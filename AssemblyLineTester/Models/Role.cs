using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    /// Represents a user role within the system (e.g. Administrator, Operator, Engineer).
    public class Role
    {
        /// Unique identifier for the Role.
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = [];
    }
}
