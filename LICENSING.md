# Panduan Lisensi AssemblyLineTester

Urutan kerja dari nol sampai aplikasi terbuka di PC client.

Ada dua peran. Perhatikan labelnya — beberapa langkah harus dikerjakan di mesin yang berbeda.

- **VENDOR** = laptop Anda (tempat kode dan `private.pem`)
- **CLIENT** = PC di pabrik yang akan dipasangi aplikasi

---

## BAGIAN 0 — Sekali seumur proyek (VENDOR)

Lewati bagian ini kalau `private.pem` sudah ada. **Menjalankan ulang `genkeys` akan menimpanya, dan semua lisensi yang sudah terbit jadi tidak bisa diterbitkan ulang.**

```bash
cd C:\Users\User\temp\AssemblyLineTester
dotnet run --project Tools/LicenseGenerator -- genkeys .
```

Menghasilkan `private.pem` dan `public.pem`, lalu mencetak satu baris Base64.

1. Salin baris Base64 itu
2. Buka `AssemblyLineTester/Services/LicenseService.cs`
3. Ganti isi `PublicKeyBase64` dengan baris tadi
4. **Backup `private.pem`** ke luar folder proyek — USB, repo privat, atau brankas password

Pastikan sepasang sebelum lanjut:

```bash
python Tools/license-doctor.py --license license.lic --key private.pem
```

Langkah 2 harus `[OK] private.pem sepasang dengan public key di aplikasi`.

> Kunci privat menandatangani, kunci publik memeriksa. Kalau tidak sepasang, aplikasi menolak setiap lisensi dengan pesan *"missing, corrupted, or was not issued for this application"* — walaupun isi lisensinya benar.

---

## BAGIAN 1 — Bangun aplikasi (VENDOR)

### 1. Publish

```bash
cd C:\Users\User\temp\AssemblyLineTester
dotnet publish AssemblyLineTester/AssemblyLineTester.csproj -p:PublishProfile=FolderProfile
```

Hasil: `AssemblyLineTester\bin\Release\net10.0-windows\publish\`

### 2. Buat installer

Butuh Inno Setup dari jrsoftware.org.

```bash
iscc Installer\AssemblyLineTester.iss
```

Hasil: `Installer\Output\AssemblyLineTester-1.0.0-setup.exe`

> Installer memuat public key yang tertanam saat publish. Kalau nanti public key diganti, installer **harus** dibuat ulang.

---

## BAGIAN 2 — Pasang di PC client (CLIENT)

### 3. Install

Jalankan `AssemblyLineTester-1.0.0-setup.exe`. Aplikasi masuk ke `C:\Program Files\AssemblyLineTester`.

### 4. Buka aplikasi sekali — sengaja dibiarkan gagal

Akan muncul dialog:

```
No license file was found for this installation.
Machine ID: 04-F0-EE-3C-5E-3D
```

Ini **normal dan diperlukan**. Dua hal terjadi di sini:

- Folder `C:\ProgramData\AssemblyLineTester` terbentuk
- Machine ID terbaca

Catat Machine ID-nya, kirim ke vendor. Kalau dialognya sudah terlanjur ditutup:

```bash
getmac /v /fo list
```

Ambil MAC adapter **fisik** — Ethernet atau Wi-Fi. Abaikan VirtualBox, VMware, Hyper-V, Bluetooth, dan WSL.

---

## BAGIAN 3 — Terbitkan lisensi (VENDOR)

### 5. Buat file lisensi

```bash
dotnet run --project Tools/LicenseGenerator -- issue --customer "Caterpillar" --machine 04-F0-EE-3C-5E-3D --expiry 2027-12-31 --key private.pem --out license.lic
```

| Argumen | Isi |
|---|---|
| `--customer` | nama pelanggan, muncul di Settings |
| `--machine` | Machine ID dari langkah 4 |
| `--expiry` | `yyyy-MM-dd`. Kosongkan untuk tanpa batas waktu |
| `--key` | `private.pem` |
| `--out` | nama file keluaran |

### 6. Periksa sebelum dikirim

```bash
python Tools/license-doctor.py --license license.lic --key private.pem
```

Langkah 1–4 dan 6 harus `[OK]`.

**Langkah 5 (Machine ID) akan GAGAL di laptop vendor — itu wajar**, karena lisensinya untuk MAC mesin client, bukan mesin Anda. Yang penting cocokkan angka yang tercetak dengan Machine ID yang dikirim client.

### 7. Kirim ke client

Kirim **hanya** `license.lic`.

**Jangan pernah kirim `private.pem`.** Siapa pun yang memegangnya bisa menerbitkan lisensi sendiri untuk mesin mana pun.

---

## BAGIAN 4 — Aktifkan (CLIENT)

### 8. Simpan file di tempat yang benar

```
C:\ProgramData\AssemblyLineTester\license.lic
```

`ProgramData`, **bukan** `Program Files`. Dua folder berbeda:

| Folder | Isinya |
|---|---|
| `C:\Program Files\AssemblyLineTester` | aplikasi (`.exe`) — dibuat installer |
| `C:\ProgramData\AssemblyLineTester` | lisensi, log, database — dibuat aplikasi |

`C:\ProgramData` **tersembunyi** di Windows. Buka lewat `Win + R`:

```bash
explorer C:\ProgramData\AssemblyLineTester
```

Pastikan namanya persis `license.lic`, bukan `license.lic.txt`. Notepad sering menambah `.txt` diam-diam — aktifkan **View → Show → File name extensions** di File Explorer untuk memeriksa.

### 9. Buka aplikasi

Langsung masuk. Tidak perlu restart Windows.

---

## Kalau masih ditolak

Jalankan diagnostiknya di PC client:

```bash
python Tools/license-doctor.py
```

Ia memeriksa enam mata rantai berurutan dan menyebut mana yang putus.

Kalau Python tidak ada di PC client, baca pesan dialognya — tiap pesan menunjuk satu penyebab:

| Pesan dialog | Artinya | Perbaikan |
|---|---|---|
| No license file was found | file tidak ada di `C:\ProgramData\AssemblyLineTester\` | ulangi langkah 8; periksa `.txt` tersembunyi |
| missing, corrupted, or was not issued for this application | tanda tangan tidak cocok | `private.pem` tidak sepasang dengan public key di build — ulangi BAGIAN 0 langkah 2, lalu publish dan install ulang |
| not issued for this machine | Machine ID beda | terbitkan ulang dengan MAC yang benar |
| expired | lewat tanggal | terbitkan ulang dengan `--expiry` baru |
| clock rolled back | jam mundur | benarkan jam Windows |
| corrupt state | `license.state` diedit | hapus `C:\ProgramData\AssemblyLineTester\license.state` |

---

## Kerja bertiga dalam satu tim

`private.pem` dan `*.lic` ada di `.gitignore` — jadi **`git pull` tidak membawa kunci**. Anggota tim yang baru clone mendapat kodenya saja.

Anggap `private.pem` sebagai stempel perusahaan: **satu untuk seluruh tim**. `PublicKeyBase64` di `LicenseService.cs` adalah cetakan cap-nya di dalam aplikasi. Stempel lain = cap tidak cocok = lisensi ditolak.

### Anggota tim ingin menjalankan aplikasi di laptopnya

Dia berperan sebagai **client**, bukan vendor. Tidak perlu `private.pem`.

1. **Dia**: jalankan aplikasi, catat Machine ID dari dialog error, kirim ke pemegang kunci
2. **Pemegang kunci**: `issue --customer "<Nama> - Dev" --machine <ID> --key private.pem --out <nama>.lic`
3. **Dia**: simpan sebagai `C:\ProgramData\AssemblyLineTester\license.lic`, buka aplikasi

### Anggota tim ingin memasang ke PC client sendiri

Dia butuh `private.pem` — **yang sama persis**, bukan bikin baru.

1. **Pemegang kunci**: kirim `private.pem` lewat USB, password manager, atau repo privat. Jangan chat, email, atau git.
2. **Dia**: taruh di root proyek, lalu buktikan sepasang:
   ```bash
   python Tools/license-doctor.py --license <apa-saja>.lic --key private.pem
   ```
   Langkah 2 wajib `[OK]`. Kalau `GAGAL`, kuncinya salah — berhenti, jangan terbitkan apa pun.
3. **Dia**: lanjutkan BAGIAN 1 sampai 4 di atas seperti biasa

### Yang akan merusak kerja semua orang

Menjalankan `genkeys`, lalu menempel public key baru ke `LicenseService.cs` dan commit. Akibatnya:

- semua lisensi yang sudah terbit mati seketika
- setiap PC client harus di-install ulang
- lisensi dev anggota tim lain ikut mati

`genkeys` sekarang menolak menimpa `private.pem` yang ada, tapi `--force` masih menembusnya. Sepakati: **hanya satu orang** yang memegang dan menyimpan kunci.

---

## Aturan yang tidak boleh dilanggar

1. **Jangan jalankan `genkeys` dua kali.** Ia menimpa `private.pem` tanpa peringatan. Semua lisensi yang beredar langsung tidak bisa diterbitkan ulang.
2. **Ganti `PublicKeyBase64` = wajib publish, buat installer, dan install ulang.** Build lama tidak akan menerima lisensi dari kunci baru.
3. **Backup `private.pem` di luar folder proyek.** Hilang berarti setiap pelanggan harus di-install ulang.
4. **`license.state` jangan disentuh.** Dibuat aplikasi, dikunci HMAC, penahan jam mundur. Tidak perlu disalin antar mesin.
