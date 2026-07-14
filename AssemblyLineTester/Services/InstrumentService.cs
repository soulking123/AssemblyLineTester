using System;
using System.Collections.Generic;
using CtaDotNet.Services;
using CtaDotNet.Models;
using CtaDotNet.Enums;

namespace AssemblyLineTester.Services
{
    public class InstrumentService
    {
        private const string ServerIPandPort = "127.0.0.1:55555";
        private const string UserName = "User1";
        private const string Password = "";

        private readonly CtaService _service = new();
        private short _connectionId = -1;

        public bool IsConnected => _connectionId != -1;
        public short ConnectionId => _connectionId;

        public bool Connect()
        {
            try
            {
                var connectionResult = _service.Connect(ServerIPandPort, UserName, Password);
                if (connectionResult.DllCallResult == CtaCommandResultCode.Success)
                {
                    _connectionId = connectionResult.FunctionResult;
                    return true;
                }
                _connectionId = -1;
                return false;
            }
            catch
            {
                _connectionId = -1;
                return false;
            }
        }

        public void Disconnect()
        {
            if (_connectionId != -1)
            {
                try
                {
                    _service.Disconnect(_connectionId, false);
                }
                finally
                {
                    _connectionId = -1;
                }
            }
        }

        /// <summary>
        /// Retrieves the configuration/wiring details of a test program.
        /// </summary>
        public CtaStringResult GetTestProgram(string testName)
        {
            if (!IsConnected) return null!;
            try
            {
                return _service.GetTest(_connectionId, testName);
            }
            catch
            {
                return null!;
            }
        }

        /// <summary>
        /// Retrieves the connection resistance limit from the active test parameters as a double.
        /// </summary>
        public double GetContinuityResistance()
        {
            if (!IsConnected) return 0.0;
            try
            {
                var result = _service.GetParameters(_connectionId);
                if (result == null || result.DllCallResult != CtaCommandResultCode.Success || string.IsNullOrEmpty(result.Result))
                {
                    return 0.0;
                }

                var lines = result.Result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    if (line.Contains("CONNECTION RESIS", StringComparison.OrdinalIgnoreCase))
                    {
                        string valuePart = line.Replace("CONNECTION RESIS", "", StringComparison.OrdinalIgnoreCase).Trim();
                        string numericStr = string.Empty;
                        foreach (char c in valuePart)
                        {
                            if (char.IsDigit(c) || c == '.' || c == '-' || c == '+')
                            {
                                numericStr += c;
                            }
                        }

                        if (double.TryParse(numericStr, System.Globalization.CultureInfo.InvariantCulture, out double val))
                        {
                            return val;
                        }
                    }
                }
            }
            catch
            {
                // Suppress exceptions
            }
            return 0.0;
        }

        /// <summary>
        /// Uploads/loads a custom test program to the tester.
        /// </summary>
        public CtaCommandResultCode PutTestProgram(string testName, string testText, bool setAsActive = true)
        {
            if (!IsConnected) return (CtaCommandResultCode)(-1);
            try
            {
                return _service.PutTest(_connectionId, testName, testText, setAsActive);
            }
            catch
            {
                return (CtaCommandResultCode)(-2);
            }
        }

        /// <summary>
        /// Executes a single test run on the instrument.
        /// </summary>
        public CtaTestResult RunSingleTest(bool runHipot, bool runShorts)
        {
            if (!IsConnected) return null!;
            try
            {
                return _service.SingleTest(_connectionId, runHipot, runShorts);
            }
            catch
            {
                return null!;
            }
        }

        /// <summary>
        /// Retrieves the real-time measured values from the last test run.
        /// </summary>
        public DllDualValueReturnObject<CtaMeasuredValuesResult> GetMeasuredValues()
        {
            if (!IsConnected) return null!;
            try
            {
                return _service.GetMeasuredValues(_connectionId);
            }
            catch
            {
                return null!;
            }
        }
    }
}
