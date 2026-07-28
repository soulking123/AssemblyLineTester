using System;
using System.Diagnostics;

namespace AssemblyLineTester.Helpers
{
    // RULE: every stored timestamp is UTC. Local time exists only where a person reads it.
    public static class AppTime
    {
        // Current instant, in the form everything is stored in.
        public static DateTime UtcNow => DateTime.UtcNow;

        // Kind is stamped first: SQLite returns Unspecified, whose conversion rule is easy to misread.
        public static DateTime ToLocal(DateTime storedUtc) =>
            DateTime.SpecifyKind(storedUtc, DateTimeKind.Utc).ToLocalTime();

        public static DateTime? ToLocal(DateTime? storedUtc) =>
            storedUtc.HasValue ? ToLocal(storedUtc.Value) : null;

        // Turns a local instant, such as a date off a picker, into storage form.
        public static DateTime ToUtc(DateTime local) =>
            DateTime.SpecifyKind(local, DateTimeKind.Local).ToUniversalTime();

        // Half-open [start, end): the end is the next local midnight, so 23:59:59.999 is included.
        public static (DateTime? FromUtc, DateTime? ToUtc) LocalDateRangeToUtc(DateTime? localFrom, DateTime? localTo)
        {
            DateTime? from = localFrom.HasValue ? ToUtc(localFrom.Value.Date) : null;
            DateTime? to = localTo.HasValue ? ToUtc(localTo.Value.Date.AddDays(1)) : null;
            return (from, to);
        }

        // Debug-only guard on the round trip and the day-range boundaries.
        [Conditional("DEBUG")]
        public static void SelfCheck()
        {
            var utc = new DateTime(2026, 7, 28, 3, 30, 0, DateTimeKind.Utc);

            Debug.Assert(ToUtc(ToLocal(utc)) == utc, "local and UTC must round trip");
            Debug.Assert(ToLocal((DateTime?)null) is null, "a missing instant must stay missing");

            var day = new DateTime(2026, 7, 28);
            var (from, to) = LocalDateRangeToUtc(day, day);

            Debug.Assert(from is not null && to is not null, "a bounded range must produce both ends");
            Debug.Assert(to!.Value - from!.Value == TimeSpan.FromDays(1), "one local day must span 24 hours");
            Debug.Assert(ToLocal(from.Value) == day, "the range must start at local midnight");
            Debug.Assert(ToLocal(to.Value) == day.AddDays(1), "the range must end at the next local midnight");

            var (openFrom, openTo) = LocalDateRangeToUtc(null, null);
            Debug.Assert(openFrom is null && openTo is null, "an unbounded range must stay unbounded");
        }
    }
}
