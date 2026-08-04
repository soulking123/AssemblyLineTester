using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;

namespace AssemblyLineTester.Services
{
    public enum LicenseValidationResult
    {
        Valid,
        MissingFile,
        InvalidSignature,
        Expired,
        WrongMachine,
        ClockRolledBack,
        CorruptState
    }

    // Verifies license.lic against the embedded public key and a clock-rollback ratchet.
    public static class LicenseService
    {
        // Public half of the keypair from Tools/LicenseGenerator's genkeys.
        private const string PublicKeyBase64 = "MIIBojANBgkqhkiG9w0BAQEFAAOCAY8AMIIBigKCAYEA13UtXXHWmT/CZJV0yw+CM4/IkgTrUeoFEb0PAbX9LNH3722Q5DFXYAQ89l00pYHFsE6a/jUOSY9vhcAD5QDWbm5FRblEKw6P00H+IuzsrnYvOTrKPmIwAWiT1MFilT+2EvdoY4I/H/b5MEnkc9BhBLaLKO69/CtcNBmuUIA1lpq7G4YB8iamYwf2Foea2L6/UAEmAIig7NOBU1aMkEhP8yd9j7dmYBZd25hPE6F7OqGvQ7neb4w0c/lRnwJ/GfbHScfddOqY7QK2tIOvTCSSPpuLqljdOEw01tGMPePrOmC10h1mQaFXtEnowrwM+xl8W7eLQ3E8TTw2VMOEjAqTc/RNW5cKzrhmtBc3wSBV3B5oYf+AhZiJy1iOAIIucQxrwtJy0RC410uuV6qIL2o+VthpqllzdZdqsyjCgteZfmvzdI6N7Bm4FWijdfVRy9h/0B6aD2Dr/J+JGPfVGByV18sv+A3Hx5X65iqStMK9o4HpQWVK6oWuHHRzqBYtm1GBAgMBAAE=";

        // Signs license.state so a hand edit is caught.
        private static readonly byte[] RatchetHmacKey = Encoding.UTF8.GetBytes("AssemblyLineTester.LicenseRatchet.v1");

        private static readonly TimeSpan ClockTolerance = TimeSpan.FromMinutes(5);

        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        public static string LicenseFilePath => Path.Combine(AppInfo.DataDirectory, "license.lic");
        public static string RatchetFilePath => Path.Combine(AppInfo.DataDirectory, "license.state");

        // Checks signature, machine lock, expiry and clock rollback, in that order.
        public static (LicenseValidationResult Result, LicensePayload? Payload) Validate()
        {
            if (!File.Exists(LicenseFilePath))
                return (LicenseValidationResult.MissingFile, null);

            string wrapperJson;
            try
            {
                wrapperJson = File.ReadAllText(LicenseFilePath);
            }
            catch
            {
                return (LicenseValidationResult.MissingFile, null);
            }

            if (!TryGetPayloadAndSignature(wrapperJson, out string payloadJson, out byte[] signature))
                return (LicenseValidationResult.InvalidSignature, null);

            if (!VerifySignature(payloadJson, signature, Convert.FromBase64String(PublicKeyBase64)))
                return (LicenseValidationResult.InvalidSignature, null);

            LicensePayload? payload;
            try
            {
                payload = JsonSerializer.Deserialize<LicensePayload>(payloadJson, JsonOptions);
            }
            catch
            {
                return (LicenseValidationResult.InvalidSignature, null);
            }

            if (payload == null)
                return (LicenseValidationResult.InvalidSignature, null);

            // Matched against every physical adapter, so a disabled NIC or a new dock does not lock the operator out.
            if (!string.IsNullOrEmpty(payload.MachineId) &&
                !AppInfo.MachineIds.Contains(AppInfo.NormalizeMachineId(payload.MachineId), StringComparer.OrdinalIgnoreCase))
                return (LicenseValidationResult.WrongMachine, payload);

            var ratchetResult = CheckAndAdvanceRatchet(RatchetFilePath, AppTime.UtcNow, out DateTime effectiveNowUtc);
            if (ratchetResult != LicenseValidationResult.Valid)
                return (ratchetResult, payload);

            if (payload.ExpiryUtc.HasValue && effectiveNowUtc > payload.ExpiryUtc.Value)
                return (LicenseValidationResult.Expired, payload);

            return (LicenseValidationResult.Valid, payload);
        }

        // Hands back the exact payload bytes that were signed, not a re-serialized copy.
        private static bool TryGetPayloadAndSignature(string wrapperJson, out string payloadJson, out byte[] signature)
        {
            payloadJson = string.Empty;
            signature = [];
            try
            {
                using var doc = JsonDocument.Parse(wrapperJson);
                var root = doc.RootElement;
                payloadJson = root.GetProperty("payload").GetString() ?? string.Empty;
                signature = Convert.FromBase64String(root.GetProperty("signature").GetString() ?? string.Empty);
                return payloadJson.Length > 0 && signature.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        private static bool VerifySignature(string payloadJson, byte[] signature, byte[] publicKeyInfo)
        {
            try
            {
                using var rsa = RSA.Create();
                rsa.ImportSubjectPublicKeyInfo(publicKeyInfo, out _);
                byte[] data = Encoding.UTF8.GetBytes(payloadJson);
                return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
            }
            catch
            {
                return false;
            }
        }

        // Advances a persisted "last seen" instant; a step backwards or a corrupt file fails closed.
        private static LicenseValidationResult CheckAndAdvanceRatchet(string statePath, DateTime nowUtc, out DateTime effectiveNowUtc)
        {
            effectiveNowUtc = nowUtc;

            if (TryReadRatchet(statePath, out long storedTicks, out bool corrupted))
            {
                var storedUtc = new DateTime(storedTicks, DateTimeKind.Utc);
                if (nowUtc < storedUtc - ClockTolerance)
                    return LicenseValidationResult.ClockRolledBack;

                effectiveNowUtc = storedUtc > nowUtc ? storedUtc : nowUtc;
            }
            else if (corrupted)
            {
                return LicenseValidationResult.CorruptState;
            }

            WriteRatchet(statePath, effectiveNowUtc.Ticks);
            return LicenseValidationResult.Valid;
        }

        private static bool TryReadRatchet(string path, out long ticks, out bool corrupted)
        {
            ticks = 0;
            corrupted = false;
            if (!File.Exists(path)) return false;

            string[] parts;
            try
            {
                parts = File.ReadAllText(path).Split('.');
            }
            catch
            {
                corrupted = true;
                return false;
            }

            if (parts.Length != 2) { corrupted = true; return false; }

            byte[] ticksBytes, mac;
            try
            {
                ticksBytes = Convert.FromBase64String(parts[0]);
                mac = Convert.FromBase64String(parts[1]);
            }
            catch
            {
                corrupted = true;
                return false;
            }

            if (ticksBytes.Length != sizeof(long))
            {
                corrupted = true;
                return false;
            }

            using var hmac = new HMACSHA256(RatchetHmacKey);
            byte[] expectedMac = hmac.ComputeHash(ticksBytes);

            if (!CryptographicOperations.FixedTimeEquals(mac, expectedMac))
            {
                corrupted = true;
                return false;
            }

            ticks = BitConverter.ToInt64(ticksBytes, 0);
            return true;
        }

        private static void WriteRatchet(string path, long ticks)
        {
            byte[] ticksBytes = BitConverter.GetBytes(ticks);
            using var hmac = new HMACSHA256(RatchetHmacKey);
            byte[] mac = hmac.ComputeHash(ticksBytes);

            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, $"{Convert.ToBase64String(ticksBytes)}.{Convert.ToBase64String(mac)}");
        }

        // Debug-only guard on the sign/verify roundtrip and the three rejection paths.
        [Conditional("DEBUG")]
        public static void SelfCheck()
        {
            using var rsa = RSA.Create(2048);
            byte[] publicKeyInfo = rsa.ExportSubjectPublicKeyInfo();

            const string payloadJson = "{\"customer\":\"Self-Check\",\"issuedUtc\":\"2026-01-01T00:00:00Z\"}";
            byte[] data = Encoding.UTF8.GetBytes(payloadJson);
            byte[] signature = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);

            Debug.Assert(VerifySignature(payloadJson, signature, publicKeyInfo), "a genuine signature must verify");

            string tamperedPayload = payloadJson.Replace("Self-Check", "Tampered!");
            Debug.Assert(!VerifySignature(tamperedPayload, signature, publicKeyInfo), "an edited payload must fail verification");

            byte[] tamperedSignature = (byte[])signature.Clone();
            tamperedSignature[0] ^= 0xFF;
            Debug.Assert(!VerifySignature(payloadJson, tamperedSignature, publicKeyInfo), "an edited signature must fail verification");

            Debug.Assert(AppInfo.NormalizeMachineId("d8:f3:bc:ba:a6:b7") == "D8F3BCBAA6B7", "punctuation and case must not change a machine id");
            Debug.Assert(AppInfo.NormalizeMachineId("D8-F3-BC-BA-A6-B7") == AppInfo.NormalizeMachineId("d8f3bcbaa6b7"), "every MAC spelling must normalise alike");
            Debug.Assert(AppInfo.FormatMachineId("D8F3BCBAA6B7") == "D8-F3-BC-BA-A6-B7", "display form must be the dashed MAC");
            Debug.Assert(AppInfo.NormalizeMachineId("LINE-PC-03").Length != 12, "a computer name must not pass as a machine id");

            string statePath = Path.Combine(Path.GetTempPath(), $"license-selfcheck-{Guid.NewGuid():N}.state");
            try
            {
                var t0 = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);
                var result0 = CheckAndAdvanceRatchet(statePath, t0, out DateTime effective0);
                Debug.Assert(result0 == LicenseValidationResult.Valid, "first run must seed the ratchet");
                Debug.Assert(effective0 == t0, "first run's effective time must be the observed time");

                var tForward = t0.AddDays(1);
                var resultForward = CheckAndAdvanceRatchet(statePath, tForward, out DateTime effectiveForward);
                Debug.Assert(resultForward == LicenseValidationResult.Valid, "a forward clock must be accepted");
                Debug.Assert(effectiveForward == tForward, "a forward clock must advance the effective time");

                var tRolledBack = t0.AddDays(-1);
                var resultBack = CheckAndAdvanceRatchet(statePath, tRolledBack, out _);
                Debug.Assert(resultBack == LicenseValidationResult.ClockRolledBack, "a rolled-back clock must be rejected");

                File.WriteAllText(statePath, "not-a-valid-ratchet-file");
                var resultCorrupt = CheckAndAdvanceRatchet(statePath, tForward, out _);
                Debug.Assert(resultCorrupt == LicenseValidationResult.CorruptState, "a hand-edited state file must be rejected");
            }
            finally
            {
                try { File.Delete(statePath); } catch { }
            }
        }
    }
}
