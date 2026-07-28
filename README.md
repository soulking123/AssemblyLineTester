# Assembly Line Harness Tester

Windows desktop application for testing wiring harnesses on the assembly line. Talks to a CTA
test instrument, records results, and produces test reports.

.NET 10 · WinForms · EF Core · SQLite

---

## Running it locally

```bash
dotnet build
dotnet run
```

The database is created on first run and lives outside the repo, so a clean checkout needs no
setup step.

---

## Where the application keeps things

| Path | Contents |
|---|---|
| `%LOCALAPPDATA%\AssemblyLineTester\users.db` | The database: users, reports, harnesses, configs |
| `%PROGRAMDATA%\AssemblyLineTester\logs\` | Daily technical logs, kept 30 days |

The database is per Windows user; the logs are per machine, because a log describes the PC and has
to survive the next shift signing in under a different Windows account.

Neither is inside the install folder, so replacing the application never touches customer data.

---

## Three rules that are expensive to break

**Migrations are append-only.** Never edit, regenerate or delete an existing migration once anyone
outside this repo has run it. Their database records which migrations it has applied by id; change
an id and that database can no longer be upgraded, only rebuilt from scratch.

**Set Windows display scaling to 100% before opening a Designer.** Visual Studio bakes the current
scaling into `AutoScaleDimensions`, and a form saved at 150% renders wrong on every other machine.
This is why `UIStyleHelper.GetHostScaleFactor` exists and why layout code multiplies by it.

**The keypair for licence signing, once it exists, never enters this repo.** Public key in the
application, private key somewhere only the release owner can reach.

---

## Adding a migration

`dotnet ef` cannot load an x86 assembly, and this project is x86 because `Libs\CtaDotNet.dll` is
32-bit. Flip the platform for the one command, then put it back:

1. In `AssemblyLineTester.csproj`, change `<PlatformTarget>x86</PlatformTarget>` to `AnyCPU`
2. `dotnet build`
3. `dotnet ef migrations add YourMigrationName --no-build`
4. Change `<PlatformTarget>` back to `x86`
5. Read the generated migration before committing — EF also picks up seed-data changes made since
   the last one, which is not always what you expected to be in there

---

## Building a release

```bash
dotnet publish -p:PublishProfile=FolderProfile
iscc Installer\AssemblyLineTester.iss
```

The installer lands in `Installer\Output\`. [Inno Setup](https://jrsoftware.org/isinfo.php) is a
free download and must be installed for the second command.

The publish is **self-contained**: the .NET runtime ships inside it. That is the difference between
an installer that works on a fresh factory PC and one that fails with a Windows dialog explaining
nothing. It costs about 250 MB in the publish folder; the installer excludes the native SQLite
binaries for Android, iOS and Linux, which brings the shipped payload down.

Before each release, bump `<Version>` in `AssemblyLineTester.csproj` **and** `AppVersion` in
`Installer\AssemblyLineTester.iss`. The version is shown in the log and in every diagnostics
bundle; a bug report carrying the wrong one costs more than the minute it takes to update.

---

## Supporting an installed copy

There is no remote access to a machine on a factory floor, so the application describes itself:

1. Ask the operator for **System Log → Export → No**, which writes a diagnostics zip
2. The zip holds the daily logs plus a summary: version, machine name, OS, instrument status
3. Stack traces from crashes are already in there — the application logs every unhandled exception
   before showing the operator a dialog

The database is deliberately **not** in the bundle. It holds test results and password hashes, and
these bundles travel over chat apps.

---

## Accounts

A fresh installation has one account, `admin`, whose password ships inside the installer and is
therefore public. It is flagged to force a password change at first sign-in, so it is a way in
during commissioning rather than a credential.

New accounts created from User Management start on a shared default password and carry the same
flag. Create them when they are about to be handed over, not weeks ahead: until the holder first
signs in, that default is all it takes to get in as them.
