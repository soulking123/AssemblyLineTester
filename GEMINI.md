# AssemblyLineTester Development Rules & Guidelines

## Tech Stack & Runtime Environment
- **Target Framework**: .NET 10.0 (`net10.0-windows`), WinForms.
- **Platform Architecture**: `PlatformTarget: x86` (requires 32-bit interop compatibility for hardware / CTA library).
- **ORM & Data Storage**: Entity Framework Core 10 with SQLite (`Microsoft.EntityFrameworkCore.Sqlite`).
- **Dependencies**: `FontAwesome.Sharp`, `PdfSharp`, `ClosedXML`, `WinForms.DataVisualization`, `BCrypt.Net-Next`, `SQLitePCLRaw.bundle_e_sqlite3`.

## Architectural Guidelines
1. **Layer Separation**:
   - Keep UI logic inside `Views/` Forms and Pages.
   - Business, testing, report, and hardware operations MUST reside inside `Services/`.
   - Data models belong in `Models/`, EF DbContext configuration in `Data/AppDbContext.cs`.
2. **Self-Check Pattern**:
   - When introducing or modifying critical services or core helpers, consider exposing a static `SelfCheck()` method that is registered in `Program.cs` startup sequence.
3. **Database Migration Rules**:
   - DO NOT run raw migrations directly against production database without automated backup. `DatabaseBackupService.RunAutomaticBackup()` must remain active before schema updates.
4. **Licensing & Security**:
   - Licensing uses RSA signature checking against hardware machine IDs (`AppInfo.PrimaryMachineId`).
   - Do NOT bypass or remove licensing checks in `Program.cs` or `LicenseService.cs`.
5. **UI Styling Standard**:
   - Always apply dynamic styling and theme tokens using `UIStyleHelper` (`ApplyTheme`, `StyleButton`, etc.). Avoid hardcoded raw control colors.
