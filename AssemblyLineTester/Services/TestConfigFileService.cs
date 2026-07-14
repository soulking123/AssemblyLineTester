using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using AssemblyLineTester.Models.Dto;

namespace AssemblyLineTester.Services
{
    public class TestConfigFileService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            // File stores numeric fields as quoted strings (e.g. "FromAddr": "1"), allow that.
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        // READS AND PARSES A TEST CONFIG FILE FROM LOCAL DISK. Root is a flat array of nets.
        public List<TestConfigNetDto> LoadConfigFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is required.", nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Config file not found.", filePath);

            string json = File.ReadAllText(filePath);
            var nets = JsonSerializer.Deserialize<List<TestConfigNetDto>>(json, JsonOptions);

            if (nets is null || nets.Count == 0)
                throw new InvalidDataException("Config file is empty or could not be parsed.");

            return nets;
        }

        // MAPS PARSED CONFIG NETS INTO LIVE TEST DETAIL ROWS. Measured value and
        // status are intentionally left blank/Pending - filled in later by execution logic.
        public List<LiveTestDetailRowDto> BuildLiveDetailRows(List<TestConfigNetDto> nets)
        {
            var rows = new List<LiveTestDetailRowDto>();

            for (int i = 0; i < nets.Count; i++)
            {
                var net = nets[i];
                rows.Add(new LiveTestDetailRowDto
                {
                    StepNo = net.SequenceNo > 0 ? net.SequenceNo : i + 1,
                    NetId = net.NetId,
                    TestPoint = $"{net.FromLabel} \u2192 {net.ToLabel}",
                    TestType = net.TestType,
                    ExpectedValue = FormatExpectedValue(net),
                    MeasuredValue = null,
                    Status = LiveTestStatus.Pending
                });
            }

            return rows;
        }

        private static string FormatExpectedValue(TestConfigNetDto net)
        {
            if (net.LowerLimit.HasValue && net.UpperLimit.HasValue)
                return $"{net.LowerLimit} - {net.UpperLimit} {net.Unit}";
            if (net.UpperLimit.HasValue)
                return $"< {net.UpperLimit} {net.Unit}";
            if (net.LowerLimit.HasValue)
                return $"> {net.LowerLimit} {net.Unit}";
            return "-";
        }
    }
}