using System;
using System.Diagnostics;
using System.IO;
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
    }
}
