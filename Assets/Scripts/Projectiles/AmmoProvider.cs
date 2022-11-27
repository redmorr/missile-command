using System;
using UnityEngine;

public class AmmoProvider : MonoBehaviour
{
    private IProjectile projectileType;
    private Pool<Missile> missilePool;

    private void Awake()
    {
        missilePool = FindObjectOfType<Pool<Missile>>();
    }

    public IProjectile GetProjectile()
    {
        return missilePool.Get();
    }
}
