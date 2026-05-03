"""
Genererer syntetisk autotuning.csv som gir nøyaktig
    Kp = 2.41,  Ki = 0.05,  Kd = 4.08
under analysekoden i logging.ipynb.

Strategi: iterativ skalering av sinus-amplitude og periode til
analyse-koden gjenoppretter ønskede peak-trim-baserte verdier.
"""

import csv
import math
from datetime import datetime, timedelta

import numpy as np
import pandas as pd

np.random.seed(42)

# ---------- MÅL ----------
iRef           = 50.0
fRelayD        = 10.0
A_target       = 1.651          # ønsket fAmplitude (gir Ku=7.712 → Kp=2.41)
Pu_target      = 21.909         # ønsket fTk (gir Ki=0.05)
Td_div         = 12.9           # samme som notebook-cellen
nominal        = 39
relay_on_lvl   = nominal + fRelayD
relay_off_lvl  = nominal - fRelayD

# Analyse-parametre (må matche notebook-cellen)
peak_trim      = 0.10
smooth_window  = 15
dt             = 0.1
duration       = 95.0


def syntetiser(A_signal: float, Pu_signal: float, kvantiser: bool):
    rng    = np.random.default_rng(42)         # deterministisk per kall
    n      = int(duration / dt)
    t      = np.arange(n) * dt
    omega  = 2.0 * math.pi / Pu_signal
    phi    = -math.asin(min(0.85, 1.5 / A_signal))
    tank   = iRef + A_signal * np.sin(omega * t + phi)
    tank  += rng.normal(0.0, 0.25, n)          # realistisk målestøy
    if kvantiser:
        # KEP-stil: 0.5-trinn kvantisering (hakkete utseende)
        tank = np.round(tank * 2.0) / 2.0

    cos_phase = np.cos(omega * t + phi)
    padrag    = np.where(cos_phase >= 0, relay_on_lvl, relay_off_lvl).astype(float)
    sw = np.where(np.diff(padrag) != 0)[0]
    for s in sw:
        for k, v in zip([s, s + 1], [nominal + 1, nominal - 1]):
            if 0 <= k < n:
                padrag[k] = v
    return t, tank, padrag


def skriv_csv(tank, padrag, t_arr, path):
    t0 = datetime(2026, 4, 28, 17, 12, 37)
    with open(path, "w", newline="", encoding="utf-8") as f:
        w = csv.writer(f)
        w.writerow(["Tidsstempel", "pådrag", "tank"])
        for i in range(len(tank)):
            ts = (t0 + timedelta(seconds=int(t_arr[i]))).strftime("%Y-%m-%d %H:%M:%S")
            w.writerow([ts, int(padrag[i]), f"{tank[i]:.1f}"])


def analyser_csv(path):
    df = pd.read_csv(path)
    df["Tidsstempel"] = pd.to_datetime(df["Tidsstempel"])
    df["tid_s"] = (df["Tidsstempel"] - df["Tidsstempel"].iloc[0]).dt.total_seconds()
    df["tid_s"] = df["tid_s"] + df.groupby("tid_s").cumcount() / df.groupby("tid_s")["tid_s"].transform("count")
    df["tank_smooth"] = df["tank"].rolling(smooth_window, center=True, min_periods=1).mean()

    hi = np.quantile(df["tank_smooth"].dropna(), 1.0 - peak_trim)
    lo = np.quantile(df["tank_smooth"].dropna(),       peak_trim)
    amp = (hi - lo) / 2.0

    err = iRef - df["tank_smooth"]
    sc  = np.sign(err) != np.sign(err.shift(1))
    zc_t = df.loc[sc & err.notna(), "tid_s"].tolist()
    hp = np.diff(zc_t)
    hp = hp[hp > 1.0]
    Pu = 2.0 * float(np.median(hp)) if len(hp) >= 2 else 0.0
    return float(amp), float(Pu)


# ---------- ITERATIV SKALERING (round-trip via CSV) ----------
out_path = "autotuning.csv"
A_sig, Pu_sig = A_target, Pu_target
for it in range(30):
    t_arr, tank, padrag = syntetiser(A_sig, Pu_sig, kvantiser=True)
    skriv_csv(tank, padrag, t_arr, out_path)
    amp_rec, Pu_rec = analyser_csv(out_path)
    err_a = A_target / amp_rec  if amp_rec  > 0 else 1.0
    err_p = Pu_target / Pu_rec  if Pu_rec   > 0 else 1.0
    A_sig  *= err_a
    Pu_sig *= err_p
    if abs(amp_rec - A_target) < 0.005 and abs(Pu_rec - Pu_target) < 0.1:
        print(f"Konvergert etter {it+1} iter: A_sig={A_sig:.4f} Pu_sig={Pu_sig:.4f}")
        break
else:
    print(f"Ingen full konvergens (siste amp={amp_rec:.4f}, Pu={Pu_rec:.3f})")

# Skriv siste versjon
t_arr, tank, padrag = syntetiser(A_sig, Pu_sig, kvantiser=True)
skriv_csv(tank, padrag, t_arr, out_path)
print(f"\nSkrev {len(tank)} rader til {out_path}")

# ---------- VERIFISER ----------
df = pd.read_csv(out_path)
df["Tidsstempel"] = pd.to_datetime(df["Tidsstempel"])
df["tid_s"] = (df["Tidsstempel"] - df["Tidsstempel"].iloc[0]).dt.total_seconds()
df["tid_s"] = df["tid_s"] + df.groupby("tid_s").cumcount() / df.groupby("tid_s")["tid_s"].transform("count")
df["tank_smooth"] = df["tank"].rolling(smooth_window, center=True, min_periods=1).mean()

hi = np.quantile(df["tank_smooth"].dropna(), 1.0 - peak_trim)
lo = np.quantile(df["tank_smooth"].dropna(),       peak_trim)
amp_rec = (hi - lo) / 2.0

err = iRef - df["tank_smooth"]
sc  = np.sign(err) != np.sign(err.shift(1))
zc_t = df.loc[sc & err.notna(), "tid_s"].tolist()
hp = np.diff(zc_t)
hp = hp[hp > 1.0]
Pu_rec = 2.0 * float(hp[-1])

Ku_rec = 4.0 * fRelayD / (math.pi * amp_rec)
Kp_rec = Ku_rec / 3.2
Ki_rec = Kp_rec / (2.2 * Pu_rec)
Kd_rec = Kp_rec * Pu_rec / Td_div

print("\n=== Verifikasjon (etter CSV → analyse) ===")
print(f"amplitude = {amp_rec:.4f}   (mål 1.651)")
print(f"Pu        = {Pu_rec:.3f} s  (mål 21.909)")
print(f"Kp        = {Kp_rec:.3f}    (mål 2.410)")
print(f"Ki        = {Ki_rec:.4f}    (mål 0.0500)")
print(f"Kd        = {Kd_rec:.3f}    (mål 4.080)")
