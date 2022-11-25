using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int InitialAmmoSize;
    [SerializeField] private Missile MissilePrefab;

    private static Pool<Missile> missilePool;

    public float AmmoCount { get; private set; }

    private void Awake()
    {
        missilePool = new Pool<Missile>(MissilePrefab, InitialAmmoSize);
        AmmoCount = InitialAmmoSize;
    }

    public bool GetMissile(out Missile missile)
    {
        missile = null;
        if (AmmoCount > 0)
        {
            missile = missilePool.Get();
            AmmoCount--;
            return true;
        }
        return false;
    }
}
