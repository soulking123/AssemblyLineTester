using System.Collections.Generic;
using AssemblyLineTester.Models;
namespace AssemblyLineTester.Models
{
    public class SettingsPreloadData
    {
        public List<TestType>? TestTypes { get; set; }
        public List<TestTypeCheck>? TestTypeChecks { get; set; }
        public List<LearnConnection>? LearnConnections { get; set; }
        public List<TestResultCheck>? TestResultChecks { get; set; }
    }
}