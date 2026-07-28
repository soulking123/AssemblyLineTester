using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssemblyLineTester.Models
{
    // One permission toggle for one role.
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;

        // Identifies the permission, e.g. "Page.Dashboard" or "Action.Reports.Export".
        public string PermissionKey { get; set; } = string.Empty;

        // Category the Settings page groups by, e.g. "Page Access".
        public string PermissionGroup { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsGranted { get; set; }
    }
}
