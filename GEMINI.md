# AssemblyLineTester - Project Guidelines & Rules

## Project Overview & Architecture
AssemblyLineTester is an industrial harness and connector testing application for assembly lines.

- **Framework**: .NET 10.0 Windows Forms (`net10.0-windows`)
- **Platform Architecture**: `x86` / `AnyCPU` (Targeted at `x86` for native `CtaDotNet.dll` compatibility)
- **Data Persistence**: SQLite via Entity Framework Core (`AppDbContext`), database file stored at `%LocalAppData%\AssemblyLineTester\users.db`
- **Native Hardware Library**: `CtaDotNet.dll` located in `Libs/`
- **Exporting**: PDF exports via `PdfSharp`, Excel exports via `ClosedXML`, Visual charts via `WinForms.DataVisualization`
- **Security & Licensing**: Password hashing via `BCrypt.Net-Next`, RSA public-key license signing (`LicenseService`, `Tools/LicenseGenerator`)

---

## Key Directories & Organization

- `AssemblyLineTester/`
  - `Data/`: `AppDbContext.cs` & schema initialization (`DbInitialize.cs`)
  - `Models/`: Domain entities and DTOs (e.g., `TestReport`, `TestConfig`, `ProductHarness`, `User`, `LicensePayload`)
  - `Services/`: Business logic, report exporters, licensing checks, hardware instrument interfaces, backups
  - `Views/`: Windows Forms main window (`MainForm`), login form (`LoginForm`), sub-pages (`Pages/`), modals (`Modals/`), custom controls (`Controls/`)
  - `Helpers/`: System diagnostics, time management, app info helpers
  - `Libs/`: Native dependencies (`CtaDotNet.dll`)
- `Tools/`
  - `LicenseGenerator/`: Console app for vendor key generation and license signing
  - `license-doctor.py`: Validation script for license integrity and key pairs
- `Docs/`: Project setup & licensing deployment guides (`Panduan-Instalasi-Lisensi.md`)
- `Installer/`: Inno Setup installer scripts (`AssemblyLineTester.iss`)

---

## Technical & Coding Invariants

### 1. Database & Migrations
- **Database Backup**: `DatabaseBackupService.RunAutomaticBackup()` MUST execute before running schema updates or migrations to prevent data corruption.
- **Model Drift**: Keep `AppDbContext` entity configurations clean and avoid suppressing `PendingModelChangesWarning`.

### 2. Licensing Safety
- **Public Key Invariant**: `LicenseService.cs` contains the embedded Base64 RSA public key matching `private.pem`. NEVER overwrite public key constants without verifying pair compatibility using `license-doctor.py`.
- **Clock Rollback & Machine ID**: Licensing enforces machine ID binding (`AppInfo.PrimaryMachineId`) and clock tamper detection.

### 3. Startup & Exception Handling
- **Crash Handlers**: `Program.cs` registers crash handlers (`Application.ThreadException` & `AppDomain.UnhandledException`).
- **Dual Logging**: Always log errors to `FileLogService` (disk file fallback) first before attempting database `SystemLogService` writes, in case the DB is inaccessible.

### 4. Encoding & PDF Generation
- `Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)` MUST be initialized at startup for `PdfSharp` (Code Page 1252 compatibility).

### 5. Build & Packaging Commands
- **Publish**: `dotnet publish AssemblyLineTester/AssemblyLineTester.csproj -p:PublishProfile=FolderProfile`
- **Installer**: `iscc Installer\AssemblyLineTester.iss`
