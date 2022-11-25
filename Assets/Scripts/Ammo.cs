using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int InitialAmmoSize;
    [SerializeField] private Missile MissilePrefab;

    public UnityAction<int> OnAmmoChanged;

    public int CurrentAmmo { get; private set; }
    public int InitialAmmo { get => InitialAmmoSize; }

    private static Pool<Missile> missilePool;

    private void Awake()
    {
        missilePool = new Pool<Missile>(MissilePrefab, InitialAmmoSize);
        CurrentAmmo = InitialAmmoSize;
    }

    public bool GetMissile(out Missile missile)
    {
        missile = null;
        if (CurrentAmmo > 0)
        {
            missile = missilePool.Get();
            CurrentAmmo--;
            OnAmmoChanged?.Invoke(CurrentAmmo);
            return true;
        }
        return false;
    }
}
