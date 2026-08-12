using System.ComponentModel.DataAnnotations;

namespace AssemblyLineTester.Models
{
    public class TestType
    {
        [Key]
        public int Id { get; set; }
        public string TestTypeName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;

        // The bit the instrument uses for this type. Wire is 0: it is the plain continuity case the
        // others are measured on top of, so it carries no bit of its own. Types the instrument has
        // no flag for stay 0 as well.
        public int TestTypeValue { get; set; }
    }
}