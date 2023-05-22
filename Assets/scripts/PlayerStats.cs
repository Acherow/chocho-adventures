using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStats
{
    public float Damage;
    public float Speed;
    public float Lifetime;
    public int Pierce;
    public int Bounce;

    public float FireSpeed;
    public int DoubleShot;
    public int TripleShot;

    public bool HasFire;
    public float BurnTime;
    public float BurnDamage;

    public bool HasLightning;
    public int ChainCount;
    public float ChainDamageReduction;
    public float ChainRange;

    public bool HasIce;
    public float FreezeTime;
    public float FreezeAmount;

    public List<Upgrade> Unlocks;

    public PlayerStats(float damage = 1, float speed = 10, float lifetime = 2, int pierce = 1, int bounce = 0, float firespeed = 0.5f, int doubleshot = 0, int tripleshot = 0, float burntime = 0, float burndamage = 0, int chaincount = 0, float chaindamagereduction = 0, float chainrange = 0, float freezetime = 0, float freezeamount = 0, List<Upgrade> unlocks = null)
    {
        Damage = damage;
        Speed = speed;
        Lifetime = lifetime;
        Pierce = pierce;
        Bounce = bounce;
        FireSpeed = firespeed;
        DoubleShot = doubleshot;
        TripleShot = tripleshot;
        BurnTime = burntime;
        BurnDamage = burndamage;
        ChainCount = chaincount;
        ChainDamageReduction = chaindamagereduction;
        ChainRange = chainrange;
        FreezeTime = freezetime;
        FreezeAmount = freezeamount;
        Unlocks = unlocks;
    }

    public PlayerStats Clone()
    {
        PlayerStats clon = new PlayerStats
        {
            Damage = Damage,
            Speed = Speed,
            Lifetime = Lifetime,
            Pierce = Pierce,
            Bounce = Bounce,
            FireSpeed = FireSpeed,
            DoubleShot = DoubleShot,
            TripleShot = TripleShot,
            HasFire = HasFire,
            BurnTime = BurnTime,
            BurnDamage = BurnDamage,
            HasLightning = HasLightning,
            ChainCount = ChainCount,
            ChainDamageReduction = ChainDamageReduction,
            ChainRange = ChainRange,
            HasIce = HasIce,
            FreezeAmount = FreezeAmount,
            FreezeTime = FreezeTime,
            Unlocks = Unlocks
        };
        return clon;
    }

    public static PlayerStats operator +(PlayerStats ps1, PlayerStats ps2)
    {
        
        List<Upgrade> ups = ps1.Unlocks ?? new List<Upgrade>();
        if (ps2.Unlocks != null)
        {
            foreach (var up in ps2.Unlocks)
            {
                if(!ups.Contains(up))
                ups.Add(up);
            }
        }

        PlayerStats sum = new PlayerStats
        {
            Damage = Mathf.Clamp(ps1.Damage + ps2.Damage, 1, 10),
            Speed = Mathf.Clamp(ps1.Speed + ps2.Speed, 1, 10),
            Lifetime = Mathf.Clamp(ps1.Lifetime + ps2.Lifetime, 1, 25),
            Pierce = Mathf.Clamp(ps1.Pierce + ps2.Pierce, 1, 10),
            Bounce = Mathf.Clamp(ps1.Bounce + ps2.Bounce, 0, 5),
            FireSpeed = Mathf.Clamp(ps1.FireSpeed + ps2.FireSpeed, 0.05f, 10),

            DoubleShot = Mathf.Clamp(ps1.DoubleShot + ps2.DoubleShot, 0, 5),
            TripleShot = Mathf.Clamp(ps1.TripleShot + ps2.TripleShot, 0, 5),

            HasFire = ps1.HasFire || ps2.HasFire,
            BurnTime = Mathf.Clamp(ps1.BurnTime + ps2.BurnTime, 0.1f, 10),
            BurnDamage = Mathf.Clamp(ps1.BurnDamage + ps2.BurnDamage, 0.1f, 5),

            HasLightning = ps1.HasLightning || ps2.HasLightning,
            ChainCount = Mathf.Clamp(ps1.ChainCount + ps2.ChainCount, 1, 5),
            ChainDamageReduction = Mathf.Clamp(ps1.ChainDamageReduction + ps2.ChainDamageReduction, 0, 1),
            ChainRange = Mathf.Clamp(ps1.ChainRange + ps2.ChainRange, 1, 15),

            HasIce = ps1.HasIce || ps2.HasIce,
            FreezeTime = Mathf.Clamp(ps1.FreezeTime + ps2.FreezeTime, 0.1f, 10),
            FreezeAmount = Mathf.Clamp(ps1.FreezeAmount + ps2.FreezeAmount, 0.1f, 10),
            Unlocks = ups
        };
        return sum;
    }
}