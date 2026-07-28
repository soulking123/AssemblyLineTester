using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AssemblyLineTester.Helpers
{
    /// <summary>
    /// Facts about this build and the machine it is running on. Everything a bug report needs
    /// in order to be matched back to the code that produced it.
    /// </summary>
    public static class AppInfo
    {
        /// <summary>Version from the csproj, e.g. "1.0.0". "unknown" if the assembly carries none.</summary>
        public static string Version { get; } = ReadVersion();

        /// <summary>Name of the PC. Travels with every log line so bundles from two sites cannot be confused.</summary>
        public static string MachineName { get; } = SafeMachineName();

        /// <summary>
        /// Per-machine application folder. Deliberately ProgramData, not LocalApplicationData:
        /// logs describe the machine, so they must survive a different Windows user signing in
        /// for the next shift.
        /// </summary>
        public static string DataDirectory { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "AssemblyLineTester");

        public static string LogDirectory { get; } = Path.Combine(DataDirectory, "logs");

        /// <summary>One line naming the build and the machine. Written at startup and into every bundle.</summary>
        public static string Describe() =>
            $"AssemblyLineTester {Version} | {MachineName} | {Environment.OSVersion} | {(Environment.Is64BitOperatingSystem ? "x64 OS" : "x86 OS")} | {(Environment.Is64BitProcess ? "x64 process" : "x86 process")}";

        private static string ReadVersion()
        {
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();

                // InformationalVersion carries the csproj <Version> verbatim; AssemblyVersion drops
                // any suffix and pads to four parts, so it is only the fallback.
                string? informational = asm
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

                if (!string.IsNullOrWhiteSpace(informational))
                {
                    // The SDK appends "+<commit sha>" when the build is inside a git repo.
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
