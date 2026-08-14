; Inno Setup script for Assembly Line Harness Tester.
;
; Build the payload first, then compile this file with the Inno Setup Compiler:
;
;   dotnet publish -p:PublishProfile=FolderProfile
;   iscc Installer\AssemblyLineTester.iss
;
; The result lands in Installer\Output\. Inno Setup is a free download from jrsoftware.org;
; nothing about this script needs the paid edition.

#define AppName        "Assembly Line Harness Tester"
#define AppShortName   "AssemblyLineTester"
#define AppVersion     "1.0.1"
#define AppPublisher   "Assembly Line Tester Team"
#define AppExeName     "AssemblyLineTester.exe"
#define PublishDir     "..\AssemblyLineTester\bin\Release\net10.0-windows\publish"

[Setup]
; AppId identifies the product across versions. Never change it, or an upgrade installs
; alongside the old copy instead of replacing it, and the customer ends up running whichever
; shortcut they happen to click.
AppId={{8E4A2C71-93D6-4F8B-A5E2-1C7B0D9F3A64}

AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#AppPublisher}
VersionInfoVersion={#AppVersion}

DefaultDirName={autopf}\{#AppShortName}
DefaultGroupName={#AppName}
DisableProgramGroupPage=yes
UninstallDisplayIcon={app}\{#AppExeName}
UninstallDisplayName={#AppName}

; The application is 32-bit because CtaDotNet.dll is, so it belongs in Program Files (x86) on a
; 64-bit machine. Leaving ArchitecturesInstallIn64BitMode empty is what puts it there.
ArchitecturesInstallIn64BitMode=

; Installing into Program Files needs elevation, and so does writing the shared data folder.
PrivilegesRequired=admin

OutputDir=Output
OutputBaseFilename={#AppShortName}-{#AppVersion}-setup
Compression=lzma2/max
SolidCompression=yes
WizardStyle=modern

; Refuses to install over a copy that is currently running, rather than half-replacing its files.
CloseApplications=yes
RestartApplications=no

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Create a desktop shortcut"; GroupDescription: "Shortcuts:"

[Files]
; Everything the publish produced, minus the native SQLite binaries for platforms this
; application will never run on. The package that supplies them ships Android, iOS, Linux and
; WebAssembly builds as well; on a Windows factory PC they are about 60 MB of dead weight.
Source: "{#PublishDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; \
    Excludes: "runtimes\android-*,runtimes\ios-*,runtimes\iossimulator-*,runtimes\linux-*,runtimes\osx*,runtimes\maccatalyst*,runtimes\browser-*,runtimes\tvos*,runtimes\unix,*.pdb"

[Dirs]
; Created up front and made writable by everyone, because the logs and the diagnostics bundle are
; written by whichever operator is signed in, not by the administrator who ran the installer.
Name: "{commonappdata}\{#AppShortName}"; Permissions: users-modify
Name: "{commonappdata}\{#AppShortName}\logs"; Permissions: users-modify

[Icons]
Name: "{group}\{#AppName}"; Filename: "{app}\{#AppExeName}"
Name: "{group}\Uninstall {#AppName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#AppName}"; Filename: "{app}\{#AppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExeName}"; Description: "Start {#AppName}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
; Only the folder the installer created. Test reports, the database and the logs are the
; customer's records and are deliberately left behind - an uninstall during a version upgrade
; must not take the production history with it.
Type: dirifempty; Name: "{app}"
