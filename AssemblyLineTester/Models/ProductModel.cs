using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class ProductModel
    {
        //[Key]
        //public int Id { get; set; }

        //public string Name { get; set; } = string.Empty;

        //public string? Desc { get; set; }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> ImagePath { get; set; } = new();
        public List<bool> IncludedForReport { get; set; } = new();
        public List<HarnessAssembly> HarnessAssemblies { get; set; } = new();
    }
}
