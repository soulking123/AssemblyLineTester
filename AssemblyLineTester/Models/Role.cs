using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = [];
    }
}
