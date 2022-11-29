using System;
using UnityEngine;

[RequireComponent(typeof(AmmoCounter))]
[RequireComponent(typeof(GunBarrelRotator))]
[RequireComponent(typeof(PlayerStructure))]
public class GroundMissileLauncher : MonoBehaviour, IAttacker
{
    [SerializeField] private ObjectPool<Projectile> missilePool;
    [SerializeField] private Transform muzzle;
    [SerializeField] private int speed;
    [SerializeField] private ExplosionStats explosionStats;

    private AmmoCounter ammoCounter;
    private GunBarrelRotator gunBarrelRotator;

    private Action<IAttacker> deregister;

    public Vector3 Position { get => transform.position; }
    public bool CanAttack { get => ammoCounter.HasAmmo; }

    private void Awake()
    {
        ammoCounter = GetComponent<AmmoCounter>();
        gunBarrelRotator = GetComponent<GunBarrelRotator>();
    }

    public void Attack(Vector3 target)
    {
        if (ammoCounter.HasAmmo)
        {
            gunBarrelRotator.RotateBarrel(target);
            Projectile missile = missilePool.Pull();
            missile.Setup(speed, 0, explosionStats);
            missile.Launch(muzzle.position, target);
            ammoCounter.SpentAmmo();

            if (!ammoCounter.HasAmmo)
                deregister?.Invoke(this);
        }
    }

    public void SetupAttacker(Action<IAttacker> action)
    {
        deregister = action;
    }

    private void OnDisable()
    {
        deregister?.Invoke(this);
    }
}
