using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class Area
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Desc { get; set; }
    }
}
