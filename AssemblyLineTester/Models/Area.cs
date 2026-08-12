using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    // A section of the machine a harness belongs to; Name is what the pages show and filter on.
    public class Area
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Desc { get; set; }
    }
}
