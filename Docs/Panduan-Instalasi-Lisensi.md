# Assembly Line Tester — Panduan Build, Distribusi, dan Lisensi

Panduan lengkap dari nol: menyiapkan folder installer, membuat file setup, mengirim ke client,
menerbitkan lisensi, sampai aplikasi berjalan — dan cara mengulanginya setiap kali ada update.

Aplikasi tidak akan jalan tanpa lisensi yang sah. Saat start, aplikasi membaca
`C:\ProgramData\AssemblyLineTester\license.lic`, memverifikasi tanda tangan RSA-nya terhadap public
key yang tertanam di aplikasi, memeriksa kunci mesin, lalu memeriksa masa berlaku terhadap ratchet
anti-mundur jam sistem. Jika salah satu gagal, muncul dialog **License Error** dan aplikasi tertutup.

---

## Peta alur

```
VENDOR                                         CLIENT
------                                         ------
Bagian 1  Siapkan tools (sekali saja)
Bagian 2  Buat folder Installer + file .iss
Bagian 3  Build  ──► setup.exe (54 MB)
Bagian 4  Ambil checksum
Bagian 5  Kirim installer  ──────────────────► Bagian 6  Install aplikasi
                                               Bagian 6  Ambil Machine ID (MAC address)
Bagian 7  Terbitkan license.lic  ◄─ Machine ID ─┘
Bagian 7  Kirim license.lic  ────────────────► Bagian 8  Salin ke ProgramData
                                               Bagian 9  Jalankan & login
Bagian 10 Update versi berikutnya ───────────► Client jalankan setup baru
```

Lisensi baru bisa dibuat **setelah** tahu Machine ID PC client. Jadi installer dulu, lisensi
menyusul. Kalau Machine ID sudah diketahui lebih awal, kirim keduanya sekaligus.

---

## Bagian 1 — Siapkan tools (sekali saja per PC vendor)

### 1.1 .NET SDK

Untuk build aplikasi. Cek sudah ada:

```bash
dotnet --version
```

Harus keluar `10.x`. Kalau perintah tidak dikenali, install .NET 10 SDK dari
<https://dotnet.microsoft.com/download>.

### 1.2 Inno Setup

Untuk membungkus hasil build jadi satu file setup. Download gratis dari
<https://jrsoftware.org/isinfo.php> — edisi berbayar tidak diperlukan.

Setelah install, program compilernya ada di:

```
C:\Program Files (x86)\Inno Setup 6\ISCC.exe
```

### 1.3 Tambahkan Inno Setup ke PATH (opsional)

Installer Inno Setup **tidak** menambahkan dirinya ke PATH. Kalau mengetik `iscc` langsung, muncul:

```
iscc : The term 'iscc' is not recognized as the name of a cmdlet, function, script file, or operable program.
```

Dua pilihan:

**Pilihan A — panggil dengan path lengkap** setiap kali. Tanda `&` wajib di PowerShell karena path
mengandung spasi:

```bash
& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" Installer\AssemblyLineTester.iss
```

**Pilihan B — daftarkan ke PATH**, sekali saja:

```bash
[Environment]::SetEnvironmentVariable("Path", [Environment]::GetEnvironmentVariable("Path","User") + ";C:\Program Files (x86)\Inno Setup 6", "User")
```

Tutup dan buka lagi PowerShell, lalu `iscc` bisa dipanggil langsung. Terminal yang sedang terbuka
tidak ikut berubah — PATH hanya dibaca saat proses dimulai.

---

## Bagian 2 — Buat folder Installer dan file resep

Bagian ini dikerjakan **sekali**. Kalau folder `Installer\` sudah ada di repository, lompat ke
Bagian 3. Kalau folder terhapus, baca dulu Bagian 11 — kemungkinan besar cukup dipulihkan dari git,
tidak perlu diketik ulang.

### 2.1 Struktur yang dituju

```
C:\Users\User\temp\AssemblyLineTester\
├── AssemblyLineTester\          ← proyek aplikasi
├── Docs\                        ← panduan ini
├── Installer\
│   ├── AssemblyLineTester.iss   ← resep installer (dibuat manual, masuk git)
│   └── Output\                  ← hasil build (dibuat otomatis, TIDAK masuk git)
│       └── AssemblyLineTester-1.0.0-setup.exe
└── Tools\
```

### 2.2 Buat foldernya

```bash
New-Item -ItemType Directory -Force C:\Users\User\temp\AssemblyLineTester\Installer
```

Folder `Output\` tidak perlu dibuat manual — Inno Setup membuatnya sendiri saat compile pertama.

### 2.3 Buat file `Installer\AssemblyLineTester.iss`

Isi persis seperti berikut:

```
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
#define AppVersion     "1.0.0"
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
```

### 2.4 Baris yang penting dipahami

| Baris | Fungsi | Boleh diubah? |
|-------|--------|---------------|
| `AppId={{8E4A2C71-...}` | Identitas produk lintas versi | **Tidak pernah.** Kalau berubah, versi baru terpasang berdampingan dengan yang lama, bukan menggantikan. Operator jalankan mana saja yang shortcut-nya kena klik |
| `#define AppVersion` | Nomor versi | Ya, naikkan tiap rilis. Harus sama dengan `<Version>` di csproj |
| `#define PublishDir` | Sumber file, relatif terhadap folder `Installer\` | Tidak, kecuali struktur folder berubah |
| `OutputDir=Output` | Tempat setup.exe jadi | Boleh |
| `Excludes:` | Buang binary SQLite untuk Android/iOS/Linux/macOS | Tidak. Itu ±60 MB sampah di PC pabrik |
| `ArchitecturesInstallIn64BitMode=` sengaja kosong | Paksa masuk `Program Files (x86)` | Tidak. Aplikasi 32-bit karena `CtaDotNet.dll` 32-bit |
| `PrivilegesRequired=admin` | Wajib elevasi | Tidak. Butuh tulis ke Program Files dan ProgramData |
| `CloseApplications=yes` | Tolak menimpa aplikasi yang sedang jalan | Tidak |

### 2.5 Commit ke git

```bash
git add Installer/AssemblyLineTester.iss
git commit -m "Add Inno Setup installer script"
```

`Installer\Output\` tidak ikut — itu hasil build, bukan sumber. Jangan commit file `.exe` ke
repository.

---

## Bagian 3 — Build installer

Dua perintah, tugasnya berbeda. Urutan tidak boleh dibalik.

```
kode C#  ──dotnet publish──►  folder publish\  ──ISCC.exe──►  setup.exe
 (edit)                       (251 MB, mentah)                (54 MB, kirim)
```

Posisikan PowerShell di root repository lebih dulu:

```bash
cd C:\Users\User\temp\AssemblyLineTester
```

### 3.1 Perintah 1 — `dotnet publish`

```bash
dotnet publish AssemblyLineTester\AssemblyLineTester.csproj -p:PublishProfile=FolderProfile
```

**Yang terjadi:** kode C# dikompilasi, lalu runtime .NET disalin ikut serta ke satu folder.

**Hasilnya:**
```
AssemblyLineTester\bin\Release\net10.0-windows\publish\
```

Isinya ±251 MB file berserakan — `AssemblyLineTester.exe`, ratusan DLL, folder bahasa. Folder ini
**belum bisa dikirim** ke client. Ini bahan mentah.

**Kenapa runtime ikut (self-contained):** PC client tidak perlu install .NET lebih dulu. Aplikasi
jalan sendiri di PC pabrik yang mungkin tidak boleh dipasangi apa-apa.

**Kenapa menunjuk langsung ke `.csproj`:** kalau dijalankan dari root tanpa menyebut proyek, seluruh
solution ikut ter-publish termasuk `Tools\LicenseGenerator`, dan muncul warning `NETSDK1198`. Tidak
berbahaya, tapi bikin output berisik.

### 3.2 Perintah 2 — `ISCC.exe`

```bash
& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" Installer\AssemblyLineTester.iss
```

**Yang terjadi:** Inno Setup membaca resep di `.iss`, mengambil seluruh isi folder `publish\`,
membuang binary platform yang tidak dipakai, lalu mengompresnya (lzma2/max) jadi **satu file**.

**Hasilnya:**
```
Installer\Output\AssemblyLineTester-1.0.0-setup.exe
```

54 MB, compile sekitar 35 detik. **File inilah yang dikirim ke client.**

### 3.3 Kesalahan paling sering

**Lupa `dotnet publish` dulu.** Installer tetap jadi tanpa error, tapi isinya **kode versi lama**.
Inno Setup hanya membungkus apa yang ada di folder `publish\` — dia tidak tahu Anda baru mengedit
kode. Gejalanya: client melapor bug yang menurut Anda sudah diperbaiki.

**Lupa tanda `&` di PowerShell.** Path mengandung spasi, tanpa `&` PowerShell membacanya sebagai
teks biasa, bukan program.

---

## Bagian 4 — Ambil checksum

```bash
certutil -hashfile Installer\Output\AssemblyLineTester-1.0.0-setup.exe SHA256
```

Keluarannya:

```
SHA256 hash of Installer\Output\AssemblyLineTester-1.0.0-setup.exe:
20a61166e18d5922862feab52349dd1cbc1e61e0b2b900d77b3be209f11fbb28
CertUtil: -hashfile command completed successfully.
```

Baris tengah itu **sidik jari** file. Satu byte berubah, seluruh 64 karakter berubah total.

**Cara pakai:**

1. Catat hash-nya, kirim bersama link download.
2. Client menjalankan perintah `certutil` yang sama di PC-nya.
3. Bandingkan 64 karakter itu.

| Hasil | Artinya |
|-------|---------|
| Sama persis | File utuh, aman di-install |
| Berbeda | Download rusak atau terpotong. Ulangi download, jangan di-install |

**Kenapa perlu:** download 54 MB yang terpotong tetap bisa diklik dan tetap membuka wizard
installer. Errornya baru muncul belakangan sebagai DLL hilang atau aplikasi crash — dan waktu
habis menebak bug yang sebenarnya tidak ada.

Checksum ini **bukan** fitur keamanan. Hanya deteksi file rusak.

---

## Bagian 5 — Kirim installer ke client

### 5.1 Pilihan pengiriman

| Cara | Kapan dipakai | Catatan |
|------|---------------|---------|
| **USB flash drive** | PC pabrik offline, teknisi datang ke lokasi | Paling aman. Bawa `license.lic` sekalian |
| **Link cloud** (Google Drive / OneDrive / Dropbox) | Client punya internet | Bagikan link, jangan attach. Beri batas waktu link |
| **WeTransfer** dan sejenisnya | Client tanpa akun cloud | Link mati 7 hari — cukup untuk sekali kirim |
| **Network share / RDP** | Satu LAN, atau remote support | Perlu VPN atau berada di lokasi yang sama |
| ~~Lampiran email~~ | ❌ | 54 MB sering melebihi batas, dan `.exe` diblokir kebanyakan mail server |
| ~~WhatsApp / Telegram~~ | ❌ | WhatsApp mengganti nama file; `.exe` sering diblokir |

### 5.2 Peringatan SmartScreen — beri tahu di depan

Installer belum ditandatangani secara digital (belum ada code-signing certificate). Windows akan
menampilkan layar biru **"Windows protected your PC"**. Client harus klik **More info** →
**Run anyway**.

Kalau tidak diberi tahu lebih dulu, client mengira installer-nya virus dan proses berhenti di situ.

Solusi permanen: beli code-signing certificate (OV sekitar $200/tahun) dan tandatangani `.exe`
dengan `signtool`.

---

## Bagian 6 — Install aplikasi di PC client

1. Jalankan `AssemblyLineTester-1.0.0-setup.exe`.
2. Klik **More info** → **Run anyway** kalau SmartScreen muncul.
3. Setujui prompt UAC — installer **wajib** administrator. Aplikasi masuk ke
   `C:\Program Files (x86)\AssemblyLineTester` (32-bit, karena `CtaDotNet.dll` 32-bit).
4. Installer membuat folder data bersama beserta izin tulisnya:

   ```
   C:\ProgramData\AssemblyLineTester\
   C:\ProgramData\AssemblyLineTester\logs\
   ```

5. **Jangan jalankan aplikasi dulu** — belum ada lisensi, hasilnya pasti dialog **License Error**.

6. **Ambil Machine ID** PC ini, kirim ke vendor.

   Machine ID adalah **MAC address adapter jaringan fisik** — bukan nama komputer. Jalankan
   aplikasi; karena belum ada lisensi, muncul dialog **License Error** yang menampilkan Machine ID
   PC tersebut:

   ```
   No license file was found for this installation.

   Machine ID: D8-F3-BC-BA-A6-B7

   Contact your AssemblyLineTester vendor for a new license file,
   quoting the machine ID above.
   ```

   Catat 12 digit itu, kirim ke vendor.

   **Alternatif tanpa membuka aplikasi** — Command Prompt:

   ```bash
   getmac /fo list /v
   ```

   Ambil MAC dari adapter fisik (Ethernet atau Wi-Fi). Abaikan adapter virtual: VMware, Hyper-V,
   VirtualBox, VPN, Bluetooth, WAN Miniport — aplikasi juga mengabaikannya.

   > **Kenapa MAC, bukan nama komputer?** Nama komputer bisa diganti kapan saja oleh siapa pun yang
   > punya akses admin, dan Windows memotongnya di 15 karakter sehingga `hostname` dan
   > `%COMPUTERNAME%` sering berbeda. MAC address melekat pada perangkat kerasnya.

   > **Kalau PC punya beberapa adapter** (Ethernet + Wi-Fi), kirim salah satu saja. Aplikasi
   > mencocokkan lisensi terhadap **semua** adapter fisik yang ada, jadi lisensi tetap sah walau
   > kabel LAN dicabut atau Wi-Fi dimatikan.

---

## Bagian 7 — Terbitkan lisensi (PC vendor)

### 7.1 Keypair: sekali seumur produk

Perintah `genkeys` hanya dijalankan **satu kali**, saat produk pertama kali disiapkan:

```bash
dotnet run --project Tools/LicenseGenerator -- genkeys
```

Hasilnya `private.pem` dan `public.pem`, plus public key versi Base64 yang dicetak di layar dan
ditempel ke `PublicKeyBase64` di `AssemblyLineTester\Services\LicenseService.cs`.

**Jangan jalankan `genkeys` lagi.** Perintah itu menimpa keypair lama, dan semua lisensi yang sudah
terbit langsung berhenti terverifikasi sampai public key baru ditempel, aplikasi di-build ulang,
didistribusikan ulang, dan semua client diterbitkan lisensi baru.

### 7.2 Terbitkan lisensi

```bash
dotnet run --project Tools/LicenseGenerator -- issue --customer "PT Contoh Manufaktur" --key private.pem --expiry 2027-12-31 --machine D8-F3-BC-BA-A6-B7 --out license.lic
```

| Argumen | Wajib | Arti |
|---------|-------|------|
| `--customer` | ya | Nama pelanggan yang dicatat di dalam lisensi |
| `--key` | ya | Path ke `private.pem` |
| `--expiry` | tidak | `yyyy-MM-dd`, dibaca sebagai UTC. Kosongkan untuk lisensi tanpa batas waktu |
| `--machine` | tidak | **MAC address** PC client. Kosongkan agar file bisa dipakai di PC mana pun |
| `--out` | tidak | Path hasil, default `license.lic` |

Format penulisan MAC bebas — ketiganya dianggap sama:

```
D8-F3-BC-BA-A6-B7      d8:f3:bc:ba:a6:b7      d8f3bcbaa6b7
```

Kalau yang dimasukkan bukan MAC (misalnya nama komputer), tool menolak sebelum menandatangani:

```
--machine 'LINE-PC-03' is not a MAC address. Expected 12 hex digits, e.g. 1C-1B-0D-9F-3A-64
```

Tool mencetak kembali nama pelanggan, masa berlaku, dan Machine ID. **Baca konfirmasi itu sebelum
mengirim file** — salah satu digit saja membuat lisensi ditolak, dan itu baru ketahuan setelah
sampai di lokasi client.

MAC melekat pada perangkat keras. Lisensi tetap sah walau nama komputer diganti atau Windows
di-install ulang. Yang membatalkannya: **penggantian motherboard atau kartu jaringan**.

### 7.3 Aturan private key

- `private.pem` **tidak pernah** dikirim, di-email, di-upload, atau di-commit. Hanya `license.lic`
  yang keluar dari PC vendor.
- Simpan `private.pem` **di luar folder repository**. Saat ini file itu ada di root repo dan hanya
  dilindungi `.gitignore` — satu `git add -f` atau satu kali zip folder sudah cukup untuk bocor.
- Backup ke media terenkripsi terpisah. Key hilang = tidak ada lisensi baru yang bisa diterbitkan
  untuk build yang sudah beredar.

### 7.4 Kirim file lisensi

`license.lic` adalah file JSON kecil. Aplikasi chat sering merusaknya: mengganti encoding, memotong
isi, atau mengubah nama jadi `license.lic.txt`.

Bungkus dalam ZIP, atau kirim lewat link cloud yang sama dengan installer.

---

## Bagian 8 — Pasang lisensi di PC client

1. Salin `license.lic` ke:

   ```
   C:\ProgramData\AssemblyLineTester\license.lic
   ```

   `ProgramData` folder tersembunyi — tempel path-nya di address bar Explorer, atau tekan `Win+R`
   lalu ketik `%ProgramData%\AssemblyLineTester`.

2. Pastikan nama file persis `license.lic`. Windows menyembunyikan ekstensi yang dikenal, jadi file
   bernama `license.lic.txt` terlihat benar di Explorer tetapi terbaca sebagai lisensi hilang. Cek
   lewat **View → File name extensions** di Explorer.

3. Jangan mengedit isi `license.lic`. Satu karakter berubah membuat tanda tangan gagal.

Lisensi milik **mesin**, bukan milik user Windows: disimpan di `ProgramData`, bukan di profil user.
Semua operator yang login di PC itu memakai file lisensi yang sama.

Setelah lisensi lolos pertama kali, aplikasi membuat `license.state` di folder yang sama. File itu
mencatat waktu terakhir aplikasi berjalan supaya jam sistem tidak bisa dimundurkan untuk
memperpanjang masa berlaku. Jangan dihapus, jangan diedit, jangan disalin ke PC lain.

---

## Bagian 9 — Jalankan dan login pertama

1. Jalankan aplikasi dari shortcut desktop atau Start Menu.
2. Sampai di layar login berarti lisensi lolos.
3. Login akun bawaan:

   | Field | Nilai |
   |-------|-------|
   | Username | `admin` |
   | Password | `Cat12345` |

4. Aplikasi langsung meminta ganti password — akun bawaan ditandai `MustChangePassword`.
   **Ganti saat serah terima**, jangan ditunda. Password bawaan ini publik dan berlaku penuh sampai
   diganti.

5. User baru yang dibuat lewat User Management juga memakai password awal `Cat12345` dengan flag
   yang sama. Buat akun mendekati waktu pemakaian, jangan jauh-jauh hari.

### Melihat detail lisensi di dalam aplikasi

**Settings → tab General → kartu LICENSE INFORMATION.** Isinya dibaca langsung dari file lisensi yang
terpasang, bukan angka contoh:

| Baris | Isinya |
|-------|--------|
| License Type | `Perpetual` kalau tanpa batas waktu, `Time-limited` kalau ada expiry |
| Machine ID | MAC address PC ini, format `D8-F3-BC-BA-A6-B7` — **inilah yang dikirim ke vendor** saat minta lisensi baru |
| Valid From | Tanggal lisensi diterbitkan |
| Valid To | Tanggal habis, atau `No expiry` |
| Licensed To | Nama pelanggan di dalam lisensi |

Kartu **SYSTEM STATUS** di halaman yang sama menampilkan baris **License Status**: `Valid` (hijau)
atau alasan penolakan berwarna merah — `Expired`, `Wrong machine`, `Invalid signature`, dan
seterusnya.

Halaman ini tempat paling cepat mengambil Machine ID untuk perpanjangan, tanpa perlu `getmac`.

### Log

Baris startup pertama di `C:\ProgramData\AssemblyLineTester\logs\` mencatat build dan mesinnya.
Lisensi yang ditolak tercatat sebagai `Aborted: license check failed (<alasan>)`.

---

## Bagian 10 — Update versi berikutnya

Aplikasi masih dikembangkan, jadi siklus ini akan berulang terus. Total 1–2 menit.

### 10.1 Naikkan versi — dua tempat, harus sama

| File | Baris |
|------|-------|
| `AssemblyLineTester\AssemblyLineTester.csproj` | `<Version>1.1.0</Version>` |
| `Installer\AssemblyLineTester.iss` | `#define AppVersion "1.1.0"` |

Wajib, bukan opsional. Nama file hasil ikut versi (`AssemblyLineTester-1.1.0-setup.exe`), jadi kalau
lupa menaikkan, installer baru menimpa yang lama di `Output\` dan client tidak bisa membedakan mana
yang mereka pasang. Versi juga muncul di log startup dan diagnostics bundle — itu yang Anda baca
saat client melapor masalah.

### 10.2 Build ulang

```bash
dotnet publish AssemblyLineTester\AssemblyLineTester.csproj -p:PublishProfile=FolderProfile
```

```bash
& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" Installer\AssemblyLineTester.iss
```

```bash
certutil -hashfile Installer\Output\AssemblyLineTester-1.1.0-setup.exe SHA256
```

### 10.3 Langkah client saat update

1. **Tutup aplikasi.** Wajib — installer menolak menimpa aplikasi yang sedang berjalan, supaya file
   tidak tergantikan setengah-setengah.
2. **Jalankan setup.exe yang baru.** Tidak perlu uninstall versi lama.
3. Klik **More info** → **Run anyway** kalau SmartScreen muncul.
4. Setujui prompt UAC.
5. Klik Next sampai selesai.

Versi lama tergantikan otomatis, bukan terpasang berdampingan. Itu efek `AppId` yang tidak pernah
diubah.

### 10.4 Data client — apa yang selamat saat update

| Isi | Nasib | Lokasi |
|-----|-------|--------|
| Lisensi | **Aman**, tidak tersentuh | `C:\ProgramData\AssemblyLineTester\license.lic` |
| Database hasil uji | **Aman** | `%LOCALAPPDATA%\AssemblyLineTester\users.db` |
| Log | **Aman** | `C:\ProgramData\AssemblyLineTester\logs\` |
| Akun user dan password | **Aman** (ikut di database) | sama seperti di atas |
| File aplikasi | Diganti versi baru | `C:\Program Files (x86)\AssemblyLineTester\` |

Semua data client ada **di luar** folder aplikasi. Itu disengaja. Bahkan uninstall pun tidak
menghapusnya — riwayat produksi tidak boleh ikut hilang saat upgrade versi.

### 10.5 Lisensi tidak ikut dikirim ulang

`license.lic` hanya dikirim **sekali**, saat instalasi pertama. Update versi tidak membatalkan
lisensi — perubahan kode biasa tidak menyentuh public key.

Lisensi baru hanya perlu diterbitkan kalau:

- Kartu jaringan atau motherboard PC client diganti
- Masa berlaku habis
- Anda menjalankan `genkeys` (ganti keypair)

### 10.6 Kalau versi baru menambah tabel atau kolom database

Otomatis, client tidak melakukan apa-apa. Saat aplikasi start pertama kali setelah update:

1. Program mem-backup database dulu.
2. Migrasi EF Core dijalankan (tabel/kolom baru dibuat).
3. Aplikasi lanjut normal.

Backup dilakukan **sebelum** migrasi karena migrasi yang menulis ulang data tidak punya tombol undo.

### 10.7 Ringkas alur update

```
VENDOR                                      CLIENT
1. edit kode
2. naikkan versi (csproj + .iss)
3. dotnet publish
4. ISCC.exe
5. certutil (ambil hash)
6. kirim setup.exe + hash ────────────────► 7. cek hash
                                            8. tutup aplikasi
                                            9. jalankan setup.exe
                                           10. buka aplikasi, langsung jalan
```

Tidak ada `license.lic` di alur ini.

---

## Bagian 11 — Kalau folder atau file terhapus

Ada dua jenis file, dua cara pulih. Cek dulu jenisnya:

```bash
git status --short Installer/
```

| Yang muncul | Artinya | Tindakan |
|-------------|---------|----------|
| `D Installer/AssemblyLineTester.iss` | Terhapus dari disk, masih ada di git | Pulihkan dari git |
| Tidak muncul sama sekali | Tidak pernah di-commit | Harus dibuat ulang (Bagian 2) |

### 11.1 File yang dilacak git — pulihkan

```bash
git restore Installer/AssemblyLineTester.iss
```

Folder `Installer\` ikut terbuat sendiri. Tidak ada isi yang hilang karena git yang jadi sumber
kebenarannya.

Untuk mengintip isinya sebelum memulihkan:

```bash
git show HEAD:Installer/AssemblyLineTester.iss
```

### 11.2 Hasil build — tidak bisa dipulihkan, harus dibuat ulang

`Installer\Output\*.exe` tidak dilacak git. Kompilasi ulang:

```bash
& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" Installer\AssemblyLineTester.iss
```

`dotnet publish` tidak perlu diulang **kalau** folder
`AssemblyLineTester\bin\Release\net10.0-windows\publish\` masih utuh. Kalau ikut terhapus, jalankan
publish dulu.

### 11.3 Kalau folder terus hilang sendiri

`iscc` hanya menulis ke `Output\`, tidak menghapus apa pun. Kalau folder hilang berulang tanpa Anda
hapus manual, curigai:

- **Antivirus** — installer yang belum ditandatangani sering dikarantina beserta foldernya. Cek
  Protection History di Windows Security.
- **Sinkronisasi cloud** (OneDrive dan sejenisnya) yang menarik penghapusan dari perangkat lain.

---

## Bagian 12 — Penanganan masalah

### 12.1 Error saat build (sisi vendor)

| Pesan | Penyebab | Perbaikan |
|-------|----------|-----------|
| `iscc : The term 'iscc' is not recognized...` | Inno Setup tidak ada di PATH | Panggil dengan path lengkap, atau daftarkan ke PATH (Bagian 1.3) |
| `The system cannot find the path specified.` (tanpa nomor baris) | Folder `Installer\` atau folder `publish\` tidak ada | Cek Bagian 11, lalu jalankan `dotnet publish` |
| `warning NETSDK1198` | Publish dijalankan untuk seluruh solution | Tunjuk langsung ke `AssemblyLineTester\AssemblyLineTester.csproj` |
| Installer jadi, tapi bug lama masih ada | Lupa `dotnet publish` sebelum `ISCC.exe` | Ulangi dua perintah dengan urutan benar |

### 12.2 Error lisensi (sisi client)

Teks dialog menunjuk langsung ke penyebabnya:

| Pesan | Penyebab | Perbaikan |
|-------|----------|-----------|
| No license file was found for this installation. | `license.lic` tidak ada, tidak terbaca, atau salah nama | Cek path persis dan pastikan ekstensinya benar-benar `.lic` |
| The license file is missing, corrupted, or was not issued for this application. | Tanda tangan gagal — file diedit, rusak saat dikirim lewat aplikasi chat, atau ditandatangani keypair berbeda dari build terpasang | Minta file baru; jangan mengedit `license.lic` manual |
| This license has expired. | Sudah lewat `--expiry` (UTC) | Terbitkan lisensi baru dengan masa berlaku lebih panjang |
| This license was issued for a different machine. | Machine ID di lisensi tidak cocok dengan MAC adapter fisik PC ini | Baca Machine ID di dialog error itu sendiri, terbitkan ulang dengan MAC tersebut |
| The system clock appears to have been moved backward, which this application does not allow. | Jam sistem dimundurkan lebih dari 5 menit dari waktu terakhir yang tercatat | Setel tanggal/jam yang benar (aktifkan sinkronisasi otomatis), lalu jalankan lagi |
| The application's saved license state could not be verified. | `license.state` rusak atau diedit manual | Hapus `C:\ProgramData\AssemblyLineTester\license.state`; aplikasi membuatnya lagi saat start berikutnya |

Catatan:

- **Masa berlaku memakai UTC.** Lisensi dengan expiry `2027-12-31` habis pada 1 Januari 2028 pukul
  07:00 WIB (UTC+7).
- **Jam mundur diingat aplikasi.** Setelah jam dimundurkan, kembalikan ke waktu sebenarnya dan error
  hilang; ada toleransi 5 menit untuk selisih sinkronisasi normal.
- **Log adalah bukti pertama.** Sebelum menebak, minta client mengirim isi
  `C:\ProgramData\AssemblyLineTester\logs\`. Database sengaja tidak ikut di diagnostics bundle karena
  berisi hasil uji dan hash password.

### 12.3 Pindah ke PC lain

- **Lisensi terkunci mesin:** tidak akan jalan di PC lain. Terbitkan lisensi baru untuk Machine ID
  PC yang baru.
- **Lisensi tanpa kunci mesin:** salin `license.lic` ke folder `ProgramData` di PC baru.

Yang **tidak** membatalkan lisensi: ganti nama komputer, install ulang Windows, ganti user Windows,
update aplikasi. Yang **membatalkan**: ganti motherboard atau kartu jaringan — MAC ikut berubah.

Salin **hanya** `license.lic`. Tinggalkan `license.state` — itu ratchet jam milik PC lama, dan
menyalinnya bisa memicu error jam mundur di PC baru.

Database ada di `%LOCALAPPDATA%\AssemblyLineTester\users.db` (per user Windows, bukan per mesin).
Itu di luar cakupan lisensi dan dipindahkan terpisah kalau memang diperlukan.

---

## Checklist

### Sekali saja, per PC vendor

- [ ] .NET 10 SDK terpasang (`dotnet --version`)
- [ ] Inno Setup terpasang
- [ ] Folder `Installer\` dan file `.iss` ada dan sudah di-commit
- [ ] `genkeys` sudah dijalankan, public key sudah ditempel di `LicenseService.cs`
- [ ] `private.pem` disimpan di luar folder repository dan sudah dibackup

### Tiap rilis

- [ ] `<Version>` di csproj dan `AppVersion` di `.iss` sudah dinaikkan dan cocok
- [ ] `dotnet publish` selesai tanpa error
- [ ] `ISCC.exe` selesai, `setup.exe` muncul di `Installer\Output\`
- [ ] SHA256 dicatat
- [ ] Installer + hash dikirim ke client
- [ ] Client diberi tahu soal layar SmartScreen

### Instalasi baru di PC client

- [ ] Checksum installer cocok
- [ ] Aplikasi terpasang sebagai administrator
- [ ] Machine ID (MAC address) dibaca dari dialog License Error dan dikirim ke vendor
- [ ] `license.lic` diterbitkan dengan pelanggan, masa berlaku, dan Machine ID yang benar
- [ ] `license.lic` ada di `C:\ProgramData\AssemblyLineTester\`
- [ ] Aplikasi jalan sampai layar login
- [ ] Login `admin` berhasil dan password bawaan sudah diganti
- [ ] Instrumen CTA terhubung dan terdeteksi
- [ ] `private.pem` tidak pernah ikut terkirim
