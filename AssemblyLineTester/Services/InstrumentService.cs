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
        private static readonly object _lockObj = new();

        public bool IsConnected => _connectionId != -1;
        public short ConnectionId => _connectionId;

        public bool Connect()
        {
            lock (_lockObj)
            {
                if (IsConnected) return true;
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
        }

        public void Disconnect()
        {
            lock (_lockObj)
            {
                if (_connectionId != -1)
                {
                    short idToDisconnect = _connectionId;
                    _connectionId = -1;

                    // Fire-and-forget so a dead socket never blocks the monitoring loop.
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        lock (_lockObj)
                        {
                            try
                            {
                                _service.Disconnect(idToDisconnect, false);
                            }
                            catch
                            {
                            }
                        }
                    });
                }
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
                return false;
            }
            try
            {
                var checkTask = System.Threading.Tasks.Task.Run(() =>
                {
                    lock (_lockObj)
                    {
                        try
                        {
                            var readyRes = _service.ServerReady(_connectionId);
                            if (readyRes == null || readyRes.DllCallResult != CtaCommandResultCode.Success || !readyRes.FunctionResult)
                            {
                                return false;
                            }

                            var infoRes = _service.GetServerInformation(_connectionId);
                            return infoRes != null && infoRes.DllCallResult == CtaCommandResultCode.Success && !string.IsNullOrWhiteSpace(infoRes.Result);
                        }
                        catch
                        {
                            return false;
                        }
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
                    // Timed out - USB most likely unplugged.
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
            lock (_lockObj)
            {
                try
                {
                    return _service.GetTest(_connectionId, testName);
                }
                catch
                {
                    return null!;
                }
            }
        }

        public double GetContinuityResistance()
        {
            if (!IsConnected) return 0.0;
            lock (_lockObj)
            {
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
                }
                return 0.0;
            }
        }

        public CtaCommandResultCode PutTestProgram(string testName, string testText, bool setAsActive = true)
        {
            if (!IsConnected) return (CtaCommandResultCode.ConnectionFailed);
            lock (_lockObj)
            {
                try
                {
                    return _service.PutTest(_connectionId, testName, testText, setAsActive);
                }
                catch
                {
                    return (CtaCommandResultCode.UnknownError);
                }
            }
        }

        public CtaTestResult RunSingleTest(bool runHipot, bool runShorts)
        {
            if (!IsConnected) return new CtaTestResult { DllCallResult = CtaCommandResultCode.ConnectionFailed };
            lock (_lockObj)
            {
                try
                {
                    return _service.SingleTest(_connectionId, runHipot, runShorts);
                }
                catch
                {
                    return new CtaTestResult { DllCallResult = CtaCommandResultCode.UnknownError };
                }
            }
        }

        public DllDualValueReturnObject<CtaTestErrorResult> GetTestErrors()
        {
            if (!IsConnected) return new DllDualValueReturnObject<CtaTestErrorResult> { DllCallResult = CtaCommandResultCode.ConnectionFailed };
            lock (_lockObj)
            {
                try
                {
                    return _service.GetTestErrors(_connectionId);
                }
                catch
                {
                    return new DllDualValueReturnObject<CtaTestErrorResult> { DllCallResult = CtaCommandResultCode.UnknownError };
                }
            }
        }

        public DllDualValueReturnObject<CtaMeasuredValuesResult> GetMeasuredValues()
        {
            if (!IsConnected) return new DllDualValueReturnObject<CtaMeasuredValuesResult> { DllCallResult = CtaCommandResultCode.ConnectionFailed };
            lock (_lockObj)
            {
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

        // Reads system information from the connected CTA Server.
        public CtaStringResult GetServerInformation()
        {
            if (!IsConnected) return new CtaStringResult { DllCallResult = CtaCommandResultCode.ConnectionFailed };
            lock (_lockObj)
            {
                try
                {
                    return _service.GetServerInformation(_connectionId);
                }
                catch
                {
                    return new CtaStringResult { DllCallResult = CtaCommandResultCode.ConnectionFailed };
                }
            }
        }

        // Starts a learn operation on the instrument for the given test name.
        public CtaCommandResultCode LearnConnection(string testName = "")
        {
            if (!IsConnected) return (CtaCommandResultCode)(-1);
            lock (_lockObj)
            {
                try
                {
                    return _service.Learn(_connectionId, testName);
                }
                catch
                {
                    return (CtaCommandResultCode)(-2);
                }
            }
        }

        public CtaCommandResultCode LearnComplexConnection(string testName, byte component)
        {
            if (!IsConnected) return (CtaCommandResultCode)(-1);
            lock (_lockObj)
            {
                try
                {
                    return _service.ComplexLearn(_connectionId, testName, component);
                }
                catch
                {
                    return (CtaCommandResultCode)(-2);
                }
            }
        }

        public CtaStringResult GetLearnedConnection(string testName = "")
        {
            if (!IsConnected) return new CtaStringResult { DllCallResult = CtaCommandResultCode.ConnectionFailed };
            lock (_lockObj)
            {
                try
                {
                    return _service.GetTest(_connectionId, testName);
                }
                catch
                {
                    return new CtaStringResult { DllCallResult = CtaCommandResultCode.ConnectionFailed };
                }
            }
        }

        public CtaStringResult GetLastError()
        {
            if (!IsConnected) return new CtaStringResult { DllCallResult = CtaCommandResultCode.ConnectionFailed };
            lock (_lockObj)
            {
                try
                {
                    return _service.GetLastError(_connectionId);
                }
                catch
                {
                    return new CtaStringResult { DllCallResult = CtaCommandResultCode.ConnectionFailed };
                }
            }
        }

        // Cancels whatever the instrument is currently running.
        public CtaCommandResultCode Abort()
        {
            if (!IsConnected) return (CtaCommandResultCode)(-1);
            lock (_lockObj)
            {
                try
                {
                    var res = _service.Abort(_connectionId);
                    return res != null ? res.DllCallResult : (CtaCommandResultCode)(-2);
                }
                catch
                {
                    return (CtaCommandResultCode)(-2);
                }
            }
        }
    }
}
