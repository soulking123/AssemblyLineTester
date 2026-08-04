using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

if (args.Length == 0)
{
    PrintUsage();
    return 1;
}

return args[0] switch
{
    "genkeys" => GenKeys(args[1..]),
    "issue" => Issue(args[1..]),
    _ => Fail($"Unknown command '{args[0]}'.")
};

static int GenKeys(string[] args)
{
    bool force = args.Contains("--force");
    string outDir = args.FirstOrDefault(a => a != "--force") ?? Directory.GetCurrentDirectory();
    Directory.CreateDirectory(outDir);

    string privatePath = Path.Combine(outDir, "private.pem");
    string publicPath = Path.Combine(outDir, "public.pem");

    // Overwriting the signing key silently strands every license already in the field: they can
    // never be reissued, and each customer needs a rebuilt installer. Refuse unless asked twice.
    if (File.Exists(privatePath) && !force)
    {
        return Fail($"""
            {privatePath} already exists. Generating new keys would destroy it, and every license
            signed with it could never be reissued - each customer would need a new installer.

            To sign a license with the existing key:
                issue --customer <name> --key "{privatePath}" ...

            If you really mean to rotate the key for every customer, back the file up first, then
            re-run with --force.
            """);
    }

    using var rsa = RSA.Create(3072);

    File.WriteAllText(privatePath, rsa.ExportRSAPrivateKeyPem());
    File.WriteAllText(publicPath, rsa.ExportSubjectPublicKeyInfoPem());

    Console.WriteLine($"Wrote {privatePath}  (keep this off the repo, off shared drives — this signs every license)");
    Console.WriteLine($"Wrote {publicPath}");
    Console.WriteLine();
    Console.WriteLine("Paste this into AssemblyLineTester/Services/LicenseService.cs as PublicKeyBase64:");
    Console.WriteLine(Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo()));

    return 0;
}

static int Issue(string[] args)
{
    string? customer = null, expiry = null, machine = null, keyPath = null;
    string outPath = "license.lic";

    for (int i = 0; i < args.Length; i++)
    {
        switch (args[i])
        {
            case "--customer": customer = args[++i]; break;
            case "--expiry": expiry = args[++i]; break;
            case "--machine": machine = args[++i]; break;
            case "--key": keyPath = args[++i]; break;
            case "--out": outPath = args[++i]; break;
            default: return Fail($"Unknown argument '{args[i]}'.");
        }
    }

    if (customer == null || keyPath == null)
    {
        return Fail("Usage: issue --customer <name> --key <private.pem> [--expiry yyyy-MM-dd] [--machine <mac>] [--out license.lic]");
    }

    string? machineId = null;
    if (machine != null)
    {
        machineId = NormalizeMachineId(machine);
        if (machineId.Length != 12)
        {
            return Fail($"--machine '{machine}' is not a MAC address. Expected 12 hex digits, e.g. 1C-1B-0D-9F-3A-64 (read it from the application's Settings page or from 'getmac').");
        }
    }

    if (!File.Exists(keyPath))
    {
        return Fail($"Private key not found: {keyPath}");
    }

    DateTime? expiryUtc = null;
    if (expiry != null)
    {
        if (!DateTime.TryParse(expiry, CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out DateTime parsed))
        {
            return Fail($"Could not parse --expiry '{expiry}'. Use yyyy-MM-dd.");
        }
        expiryUtc = parsed;
    }

    var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
    var payload = new { customer, issuedUtc = DateTime.UtcNow, expiryUtc, machineId };
    string payloadJson = JsonSerializer.Serialize(payload, jsonOptions);

    using var rsa = RSA.Create();
    rsa.ImportFromPem(File.ReadAllText(keyPath));

    byte[] signature = rsa.SignData(Encoding.UTF8.GetBytes(payloadJson), HashAlgorithmName.SHA256, RSASignaturePadding.Pss);

    var wrapper = new { payload = payloadJson, signature = Convert.ToBase64String(signature) };
    File.WriteAllText(outPath, JsonSerializer.Serialize(wrapper, new JsonSerializerOptions { WriteIndented = true }));

    Console.WriteLine($"Wrote {outPath}");
    Console.WriteLine($"Customer: {customer}");
    Console.WriteLine($"Expiry (UTC): {(expiryUtc.HasValue ? expiryUtc.Value.ToString("u", CultureInfo.InvariantCulture) : "never")}");
    if (machineId != null) Console.WriteLine($"Locked to machine: {FormatMachineId(machineId)}");
    Console.WriteLine();
    Console.WriteLine("To reissue after a key rotation, re-run with the same --customer/--expiry/--machine.");

    return 0;
}

// Must match AppInfo.NormalizeMachineId; this project deliberately does not reference the application.
static string NormalizeMachineId(string raw) =>
    new string(raw.Where(Uri.IsHexDigit).ToArray()).ToUpperInvariant();

static string FormatMachineId(string id) =>
    string.Join('-', Enumerable.Range(0, 6).Select(i => id.Substring(i * 2, 2)));

static int Fail(string message)
{
    Console.Error.WriteLine(message);
    return 1;
}

static void PrintUsage()
{
    Console.WriteLine("""
        AssemblyLineTester license tool. Commands:

          genkeys [outDir]
              Generates private.pem/public.pem (default: current directory). Run once,
              then paste the printed public key into LicenseService.PublicKeyBase64.

          issue --customer <name> --key <private.pem> [--expiry yyyy-MM-dd] [--machine <mac>] [--out license.lic]
              Builds and signs a license.lic. Drop the result at
              %ProgramData%\AssemblyLineTester\license.lic on the customer's machine.

              --machine takes the MAC address of a physical adapter, in any punctuation:
              1C-1B-0D-9F-3A-64, 1c:1b:0d:9f:3a:64 and 1c1b0d9f3a64 are the same machine.
              The customer reads it off the application's Settings page, or from the
              License Error dialog if the application will not start yet.
        """);
}
