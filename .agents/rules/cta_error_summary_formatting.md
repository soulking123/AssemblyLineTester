# Rule: CTA Test Error Summary Formatting Invariants

## 1. Scope and Invariant
Never restrict `ShowSummary` or the error display in `TestExecution.cs` to only `SHORT` errors. All errors returned in `_instrument.GetTestErrors().ObjectReturnValue.ErrorList` (Short, Open, Miswire, Diode, Resistor, Component, etc.) must be processed and displayed in the summary list.

## 2. Message Formatting Invariant
- Format test error messages using `error.Message` as the base, stripped of the `#\d+\s*` prefix (e.g. `#3 Short ...` -> `Short ...`, `#1 Open ...` -> `Open ...`).
- Convert connector coordinates (`J{conn}-{pin}`) and raw addresses into linear pin notation `Pin X` (`(conn - 1) * 64 + pin`) using `ConvertPointToPinNumber`.
- If the CTA message uses net identifiers (e.g., `Short Net2 Net6 0.1 Ohm` or `Open Net1 Net2`), replace the net names with the linear pin numbers derived from `error.Point1` and `error.Point2`.
- Pin pairs must be separated by a single space without hyphens (e.g. `Short Pin 3 Pin 4 0.1 Ohm`, `Open Pin 1 Pin 193`).
- Preserve any measured values/units (resistance, voltage) present in the message, or fallback to `error.Value1` if absent in short error messages.

## 3. Section Headers & Pass/Fail Integrity
- Group errors into `SHORT CONNECTION ERRORS` (if short errors exist) and `TEST ERRORS` (if open, miswire, or component errors exist).
- Any error present in `ErrorList` must immediately cause `allPass` and `overallResult` to evaluate to `FAIL`.
- Both `SHORT CONNECTION ERRORS` and `TEST ERRORS` sections must be supported across UI list boxes, PDF export (`ReportPdfExportService`), and Excel export (`ReportExcelExportService`) in dark red font.
