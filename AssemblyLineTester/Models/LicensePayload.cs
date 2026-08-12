using System;

namespace AssemblyLineTester.Models
{
    public sealed record LicensePayload
    {
        public string Customer { get; init; } = "";
        public DateTime IssuedUtc { get; init; }
        public DateTime? ExpiryUtc { get; init; }

        public string? MachineId { get; init; }
    }
}
