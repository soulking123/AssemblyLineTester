using System;
using System.Collections.Generic;
using CtaDotNet.Services;
using CtaDotNet.Models;
using CtaDotNet.Enums;
using System.Diagnostics;

namespace AssemblyLineTester.Services
{
    public class InstrumentService
    {
        private const string ServerIPandPort = "127.0.0.1:55555";
        private const string UserName = "User1";
        private const string Password = "";

        private static readonly CtaService _service = new();
        private static short _connectionId = -1;

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
                short idToDisconnect = _connectionId;
                _connectionId = -1; // Reset memory state instantly

                // Run native DLL disconnect in a fire-and-forget background task
                // so it doesn't block the calling monitoring loop thread
                System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        _service.Disconnect(idToDisconnect, false);
                    }
                    catch
                    {
                        // Ignore exceptions on dead socket disconnects
                    }
                });
            }
        }

        public static bool IsOnline { get; private set; }
        public static event Action<bool>? OnConnectionStatusChanged;
        private static System.Threading.CancellationTokenSource? _monitorCts;

        public static void StartMonitoring()
        {
            _monitorCts?.Cancel();
            _monitorCts = new System.Threading.CancellationTokenSource();
            var token = _monitorCts.Token;

            System.Threading.Tasks.Task.Run(async () =>
            {
                var service = new InstrumentService();
                while (!token.IsCancellationRequested)
                {
                    bool currentStatus = await service.CheckConnectionStatusAsync();

                    if (currentStatus != IsOnline)
                    {
                        IsOnline = currentStatus;
                        OnConnectionStatusChanged?.Invoke(IsOnline);
                    }

                    await System.Threading.Tasks.Task.Delay(3000, token);
                }
            }, token);
        }

        public static void StopMonitoring()
        {
            _monitorCts?.Cancel();
        }

        public async System.Threading.Tasks.Task<bool> CheckConnectionStatusAsync()
        {
            if (_connectionId == -1)
            {
                // Return offline immediately without attempting to connect
                return false;
            }
            try
            {
                var checkTask = System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        // 1. Verify CTA Server is ready to receive commands
                        var readyRes = _service.ServerReady(_connectionId);
                        //Debug.WriteLine($"ctaServerReady = {readyRes.FunctionResult}"); 


                        if (readyRes == null || readyRes.DllCallResult != CtaCommandResultCode.Success || !readyRes.FunctionResult)
                        {
                            return false;
                        }

                        // 2. Verify hardware connectivity by querying server information
                        var infoRes = _service.GetServerInformation(_connectionId);
                        //Debug.WriteLine(infoRes.Result);
                        return infoRes != null && infoRes.DllCallResult == CtaCommandResultCode.Success && !string.IsNullOrWhiteSpace(infoRes.Result);
                    }
                    catch
                    {
                        return false;
                    }
                });

                var completedTask = await System.Threading.Tasks.Task.WhenAny(checkTask, System.Threading.Tasks.Task.Delay(2000));

                if (completedTask == checkTask)
                {
                    bool isReady = await checkTask;
                    if (!isReady)
                    {
                        Disconnect();
                        return false;
                    }
                    return true;
                }
                else
                {
                    // Timeout! USB probably unplugged
                    Disconnect();
                    return false;
                }
            }
            catch
            {
                Disconnect();
                return false;
            }
        }

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

        /// <summary>
        /// Retrieves system information from the connected CTA Server.
        /// </summary>
        public CtaStringResult GetServerInformation()
        {
            if (!IsConnected) return null!;
            try
            {
                return _service.GetServerInformation(_connectionId);
            }
            catch
            {
                return null!;
            }
        }
    }
}
