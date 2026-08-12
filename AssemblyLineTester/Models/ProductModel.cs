using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    // One machine model a harness can be built for; Name is what the pages show and filter on.
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Desc { get; set; }
    }
}
