using System.Text.Json.Serialization;

namespace AssemblyLineTester.Models.Dto
{
    // Maps directly to the flat JSON array format produced by the config tool,
    // e.g. AutomatedTestConfig3.json / AutomatedTestConfig4.json.
    public class TestConfigNetDto
    {
        [JsonPropertyName("No")]
        public int SequenceNo { get; set; }

        [JsonPropertyName("HarnessGroup")]
        public string HarnessGroup { get; set; } = string.Empty;

        [JsonPropertyName("NetId")]
        public string NetId { get; set; } = string.Empty;

        [JsonPropertyName("FromLabel")]
        public string FromLabel { get; set; } = string.Empty;

        [JsonPropertyName("FromAddr")]
        public int FromAddress { get; set; }

        [JsonPropertyName("ToLabel")]
        public string ToLabel { get; set; } = string.Empty;

        [JsonPropertyName("ToAddr")]
        public int ToAddress { get; set; }

        [JsonPropertyName("TestType")]
        public string TestType { get; set; } = string.Empty;

        [JsonPropertyName("LowerLimit")]
        public double? LowerLimit { get; set; }

        [JsonPropertyName("UpperLimit")]
        public double? UpperLimit { get; set; }

        [JsonPropertyName("Unit")]
        public string Unit { get; set; } = string.Empty;
    }
}