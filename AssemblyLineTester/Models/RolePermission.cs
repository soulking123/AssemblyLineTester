using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssemblyLineTester.Models
{
    /// <summary>
    /// Represents a granular permission setting assigned to a specific role.
    /// </summary>
    public class RolePermission
    {
        /// <summary>
        /// Unique identifier for the RolePermission record.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key referring to the parent Role.
        /// </summary>
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;

        /// <summary>
        /// Unique key identifying the permission (e.g., "Page.Dashboard", "Action.TestExecution.Start").
        /// </summary>
        public string PermissionKey { get; set; } = string.Empty;

        /// <summary>
        /// Category grouping of the permission (e.g., "Page Access", "Test Operations", "System & Logs").
        /// </summary>
        public string PermissionGroup { get; set; } = string.Empty;

        /// <summary>
        /// User-friendly description shown in the UI.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Whether this permission is currently granted to the role.
        /// </summary>
        public bool IsGranted { get; set; }
    }
}
