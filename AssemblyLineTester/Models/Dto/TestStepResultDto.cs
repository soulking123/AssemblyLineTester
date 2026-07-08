namespace AssemblyLineTester.Models.Dto
{
    public class TestStepResultDto
    {
        public int StepNo { get; set; }
        public string TestType { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty; // PASS / FAIL
        public string Time { get; set; } = "-";             
    }
}