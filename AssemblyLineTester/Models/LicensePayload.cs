using System;

namespace AssemblyLineTester.Models
{
    // The signed fields inside license.lic. MachineId null means unlocked; ExpiryUtc null means perpetual.
    public sealed record LicensePayload
    {
        public string Customer { get; init; } = "";
        public DateTime IssuedUtc { get; init; }
        public DateTime? ExpiryUtc { get; init; }

        // MAC address of a physical adapter, hex only and uppercase.
        public string? MachineId { get; init; }
    }
}
