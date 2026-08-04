"""Checks every link between the signing key and a license file, and names the one that breaks.

Run on the vendor machine to check a license before sending it:
    python Tools/license-doctor.py --license license.lic --key private.pem

Run on the client machine to check the installed license:
    python Tools/license-doctor.py
"""

import argparse
import base64
import json
import os
import re
import subprocess
import sys
from datetime import datetime, timezone

from cryptography.hazmat.primitives.asymmetric import padding
from cryptography.hazmat.primitives.serialization import (
    Encoding, PublicFormat, load_der_public_key, load_pem_private_key)
from cryptography.hazmat.primitives import hashes

LICENSE_SERVICE = os.path.join(
    os.path.dirname(os.path.dirname(os.path.abspath(__file__))),
    "AssemblyLineTester", "Services", "LicenseService.cs")

INSTALLED_LICENSE = r"C:\ProgramData\AssemblyLineTester\license.lic"

failures = 0


def check(label, ok, detail=""):
    global failures
    if not ok:
        failures += 1
    print(f"  [{'OK ' if ok else 'GAGAL'}] {label}" + (f"  -- {detail}" if detail else ""))
    return ok


def embedded_public_key():
    source = open(LICENSE_SERVICE, encoding="utf-8-sig").read()
    match = re.search(r'PublicKeyBase64\s*=\s*"([^"]+)"', source)
    return match.group(1) if match else None


def local_mac_addresses():
    out = subprocess.run(["getmac", "/fo", "csv", "/nh"], capture_output=True, text=True).stdout
    return {re.sub(r"[^0-9A-F]", "", m.upper())
            for m in re.findall(r"([0-9A-Fa-f]{2}(?:[-:][0-9A-Fa-f]{2}){5})", out)}


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--license", default=INSTALLED_LICENSE)
    parser.add_argument("--key", help="private.pem used to sign, if checking before sending")
    args = parser.parse_args()

    print(f"\nLicenseService.cs : {LICENSE_SERVICE}")
    print(f"License file      : {args.license}\n")

    print("1. Public key tertanam di aplikasi")
    embedded = embedded_public_key()
    if not check("terbaca dari LicenseService.cs", embedded is not None):
        sys.exit(1)
    print(f"        sidik jari: ...{embedded[-24:]}")

    if args.key:
        print("\n2. Pasangan kunci penandatangan")
        signer = load_pem_private_key(open(args.key, "rb").read(), password=None)
        signer_pub = base64.b64encode(
            signer.public_key().public_bytes(Encoding.DER, PublicFormat.SubjectPublicKeyInfo)).decode()
        print(f"        sidik jari: ...{signer_pub[-24:]}")
        check(f"{args.key} sepasang dengan public key di aplikasi", signer_pub == embedded,
              "" if signer_pub == embedded else "aplikasi harus di-rebuild dengan public key ini, atau tanda tangani ulang dengan kunci lama")

    print("\n3. File lisensi")
    if not check("file ada", os.path.exists(args.license)):
        sys.exit(1)
    wrapper = json.load(open(args.license))
    if not check("berisi payload dan signature", "payload" in wrapper and "signature" in wrapper):
        sys.exit(1)
    payload = json.loads(wrapper["payload"])
    print(f"        customer : {payload.get('customer')}")
    print(f"        machineId: {payload.get('machineId')}")
    print(f"        expiry   : {payload.get('expiryUtc')}")

    print("\n4. Tanda tangan")
    key = load_der_public_key(base64.b64decode(embedded))
    try:
        key.verify(base64.b64decode(wrapper["signature"]), wrapper["payload"].encode(),
                   padding.PSS(mgf=padding.MGF1(hashes.SHA256()), salt_length=32), hashes.SHA256())
        check("cocok dengan public key aplikasi", True)
    except Exception:
        check("cocok dengan public key aplikasi", False,
              "ditandatangani kunci lain -- ini yang bikin pesan 'missing, corrupted, or was not issued'")

    print("\n5. Machine ID")
    wanted = re.sub(r"[^0-9A-F]", "", (payload.get("machineId") or "").upper())
    if not wanted:
        check("lisensi tidak dikunci ke mesin", True, "berlaku di mesin mana pun")
    else:
        macs = local_mac_addresses()
        check(f"{wanted} ada di mesin ini", wanted in macs,
              f"MAC mesin ini: {', '.join(sorted(macs)) or 'tidak terbaca'}")

    print("\n6. Masa berlaku")
    expiry = payload.get("expiryUtc")
    if not expiry:
        check("tanpa kedaluwarsa", True)
    else:
        when = datetime.fromisoformat(expiry.replace("Z", "+00:00"))
        check(f"belum lewat ({when:%Y-%m-%d})", when > datetime.now(timezone.utc))

    print(f"\n{'SEMUA LOLOS - lisensi ini akan diterima.' if failures == 0 else f'{failures} pemeriksaan gagal (lihat GAGAL di atas).'}\n")
    sys.exit(1 if failures else 0)


if __name__ == "__main__":
    main()
