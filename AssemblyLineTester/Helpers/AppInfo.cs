using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;

namespace AssemblyLineTester.Helpers
{
    // Facts a bug report needs to be matched back to the build that produced it.
    public static class AppInfo
    {
        // Version from the csproj, "unknown" if the assembly carries none.
        public static string Version { get; } = ReadVersion();

        // Name of the PC, carried on every log line.
        public static string MachineName { get; } = SafeMachineName();

        // MAC addresses of the physical adapters; a licence is locked to one of these.
        public static IReadOnlyList<string> MachineIds { get; } = ReadMachineIds();

        // The one quoted when requesting a licence, and shown on the Settings page.
        public static string PrimaryMachineId { get; } = MachineIds.Count > 0 ? MachineIds[0] : "";

        // ProgramData, not LocalApplicationData: logs must survive the next shift's Windows user.
        public static string DataDirectory { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "AssemblyLineTester");

        public static string LogDirectory { get; } = Path.Combine(DataDirectory, "logs");

        // One line naming the build and the machine.
        public static string Describe() =>
            $"AssemblyLineTester {Version} | {MachineName} | {Environment.OSVersion} | {(Environment.Is64BitOperatingSystem ? "x64 OS" : "x86 OS")} | {(Environment.Is64BitProcess ? "x64 process" : "x86 process")}";

        private static string ReadVersion()
        {
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();

                // InformationalVersion is verbatim; AssemblyVersion pads to four parts.
                string? informational = asm
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

                if (!string.IsNullOrWhiteSpace(informational))
                {
                    // The SDK appends "+<sha>" inside a git repo.
                    int plus = informational.IndexOf('+');
                    return plus > 0 ? informational[..plus] : informational;
                }

                return asm.GetName().Version?.ToString() ?? "unknown";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AppInfo.ReadVersion failed: {ex}");
                return "unknown";
            }
        }

        private static string SafeMachineName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AppInfo.SafeMachineName failed: {ex}");
                return "unknown";
            }
        }

        // Hex only, uppercase - the shape LicenseGenerator writes into a licence.
        public static string NormalizeMachineId(string? raw) =>
            new string((raw ?? "").Where(Uri.IsHexDigit).ToArray()).ToUpperInvariant();

        // AABBCCDDEEFF -> AA-BB-CC-DD-EE-FF, for display only.
        public static string FormatMachineId(string id) =>
            id.Length == 12
                ? string.Join('-', Enumerable.Range(0, 6).Select(i => id.Substring(i * 2, 2)))
                : id;

        // Virtual adapters come and go with VPNs and hypervisors, so a licence must never ride on one.
        private static bool IsVirtual(string description)
        {
            string[] markers = ["virtual", "vmware", "hyper-v", "vbox", "tap-", "tunnel", "loopback", "pseudo", "bluetooth", "wan miniport"];
            return markers.Any(m => description.Contains(m, StringComparison.OrdinalIgnoreCase));
        }

        // Sorted so the same adapter stays "primary" across reboots and cable changes.
        private static string[] ReadMachineIds()
        {
            try
            {
                return NetworkInterface.GetAllNetworkInterfaces()
                    .Where(n => n.NetworkInterfaceType is NetworkInterfaceType.Ethernet or NetworkInterfaceType.Wireless80211)
                    .Where(n => !IsVirtual(n.Description))
                    .Select(n => n.GetPhysicalAddress().ToString())
                    .Where(mac => mac.Length == 12)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(mac => mac, StringComparer.Ordinal)
                    .ToArray();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AppInfo.ReadMachineIds failed: {ex}");
                return [];
            }
        }
    }
}
