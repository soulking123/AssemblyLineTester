namespace AssemblyLineTester.Models.Dto
{
    public class TestDetailDto
    {
        public string NetId { get; set; } = string.Empty;
        public string HarnessGroup { get; set; } = string.Empty;
        public string FromPin { get; set; } = string.Empty;
        public string ToPin { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string TestType { get; set; } = string.Empty;
        public string Limit { get; set; } = string.Empty;
        public string Measured { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty; // PASS / FAIL / OPEN
        public string? ErrorDetail { get; set; }
    }
}