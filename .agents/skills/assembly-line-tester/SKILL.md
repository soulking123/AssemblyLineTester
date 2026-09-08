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

---

## 6. Cirris 8100 vs 1100 Program Syntax & Pin Addressing

### Pin Addressing & Mapping Formulas (448 Channels)
- **Connectors**: 7 connectors labeled `J1` through `J7`, each containing 64 pins (1–64).
- **Linear Address to Pin**:
  - Connector: `conn = ((address - 1) / 64) + 1` (1 to 7)
  - Pin: `pin = ((address - 1) % 64) + 1` (1 to 64)
  - Format: `J{conn}-{pin}` (unpadded, e.g. `J1-1`, `J4-1`). Do not emit leading `$` in export text.
- **Pin to Linear Address**:
  - Formula: `address = (conn - 1) * 64 + pin`
  - Parser tolerates both `J{conn}-{pin}` and `$J{conn}-{pin}`.

### Cirris 8100 (EasyWire) Syntax Structure
The 8100 format uses uppercase block sections delimited by `BEGIN` and `END`:
1. `TESTDEFAULTS ... END`: Tester model (`8100, 448, AHED-64`), display flags, and defaults.
2. `GRAPHIC_CONN_TYPES ... END`: Graphic layouts for connectors.
3. `INTERFACING ... END`: Declares active connectors `J1` through `J7`.
4. `ATTACH ... END`: Pin assignments per connector (`J1 , 1, 64` up to `J7 , 385, 448`).
5. `PARAMETERS ... END`: Global limits (`WIRERES , 6.0 Ohm`, `SHORTSRES , 500 kOhm`, `RESISTORRES`).
6. `INSTRUCTIONS ... END`: Instructions per wire or component:
   - `WIRE, J1-1, J4-1, 6.0 Ohm`
   - `RESISTOR, J1-3, J1-4, 100 Ohm, 10 %`
   - `DIODE, J1-5, J1-6, 0.70 V`
   - `CAPACITOR, J1-7, J1-8, 10 uF, 20 %`
7. `NETNAMES ... END`: Maps net names to pins (`Net1 , J1-1`).
8. `CUSTOM_LABELS ... END`.

### Cirris 1100 Legacy Syntax Structure
- Delimited by parentheses `(` and `)`.
- Uses `CONNECTION RESIS  <val> ohm`, `LV INSULATION RESIS  <val> ohm`, `** TEST METHOD SINGLE`, and `CHECK COMPONENTS` blocks.


