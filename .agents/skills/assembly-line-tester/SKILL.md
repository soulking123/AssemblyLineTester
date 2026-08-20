---
name: assembly-line-tester
description: >-
  Development, build, licensing, database migration, and diagnostic workflows
  for the AssemblyLineTester WinForms application.
---

# AssemblyLineTester Skill

This skill provides step-by-step guidance for building, testing, generating licenses, running database migrations, and diagnosing issues in AssemblyLineTester.

## 1. Build and Run Commands

### Build the Main Application
```powershell
dotnet build AssemblyLineTester/AssemblyLineTester.csproj -c Debug
```

### Run the Application
```powershell
dotnet run --project AssemblyLineTester/AssemblyLineTester.csproj
```

---

## 2. License Generation & Diagnosis

### Generate a License Key / File
To generate a license for testing or deployment:
```powershell
dotnet run --project Tools/LicenseGenerator/LicenseGenerator.csproj -- <MACHINE_ID> <DURATION_DAYS>
```

### Diagnose License File Issues
To inspect a license file and verify signature and expiry:
```powershell
python Tools/license-doctor.py
```

---

## 3. Database Migrations & Backup

### Add a New Entity Framework Migration
```powershell
dotnet ef migrations add <MigrationName> --project AssemblyLineTester/AssemblyLineTester.csproj
```

### Apply Migrations Manually
```powershell
dotnet ef database update --project AssemblyLineTester/AssemblyLineTester.csproj
```

---

## 4. Installer Packaging

The Inno Setup script is located at `Installer/AssemblyLineTester.iss`.
To build the installer, open `Installer/AssemblyLineTester.iss` in Inno Setup Compiler or run `ISCC.exe Installer/AssemblyLineTester.iss`.

---

## 5. Cirris EasyWire & CTA Hardware Integration

### Environment & Paths
- **Default Install Path**: `C:\Program Files (x86)\Cirris\easywire`
- **Key Binaries**: `easywire.exe`, `EWLauncher.exe`, `CirrisDataAccessService.exe`, `ArchivedReportViewer.exe`
- **Drivers**: Silicon Labs CP210x Universal Driver (`Silicon_Labs_Driver/`), USBXpress (`SiUSBXp.dll`), and Common Drivers (`CommonDrivers/`).

### CTA Server Communication
- **Endpoint**: `127.0.0.1:55555` (TCP/IP local server)
- **Default User**: `User1`
- **Locking Policy**: All hardware commands are synchronized across `_lockObj`. Background polling tasks must use non-blocking checks (`Monitor.TryEnter`) to avoid false disconnects during multi-wire testing.

