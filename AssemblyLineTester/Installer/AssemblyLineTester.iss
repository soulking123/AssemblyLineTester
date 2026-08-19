; Publish first: dotnet publish -c Release /p:PublishProfile=FolderProfile
; Output goes to bin\Release\net10.0-windows\publish\ (win-x86, self-contained).
; Then compile this script with Inno Setup (ISCC.exe or the IDE).

[Setup]
AppName=AssemblyLineTester
AppVersion=1.0.0
AppPublisher=AssemblyLineTester
DefaultDirName={autopf}\AssemblyLineTester
DefaultGroupName=AssemblyLineTester
DisableProgramGroupPage=yes
OutputDir=Output
OutputBaseFilename=AssemblyLineTester-Setup
Compression=lzma2
SolidCompression=yes
SetupIconFile=..\Assets\cat_logo.ico
UninstallDisplayIcon={app}\AssemblyLineTester.exe
ArchitecturesAllowed=x86compatible
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: desktopicon; Description: "Create a desktop shortcut"; GroupDescription: "Additional icons:"

[Files]
; Excludes the ~60 MB of Android/iOS/Linux SQLite natives the package drags in - only win-x86 runs here.
Source: "..\bin\Release\net10.0-windows\publish\*"; DestDir: "{app}"; Flags: recursesubdirs ignoreversion; Excludes: "runtimes\android*,runtimes\ios*,runtimes\linux*,runtimes\maccatalyst*,runtimes\tvos*,runtimes\browser*"

[Icons]
Name: "{group}\AssemblyLineTester"; Filename: "{app}\AssemblyLineTester.exe"
Name: "{group}\Uninstall AssemblyLineTester"; Filename: "{uninstallexe}"
Name: "{autodesktop}\AssemblyLineTester"; Filename: "{app}\AssemblyLineTester.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\AssemblyLineTester.exe"; Description: "Launch AssemblyLineTester"; Flags: nowait postinstall skipifsilent
