using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStructure))]
public class GroundMissileLauncher : MonoBehaviour, IAttacker
{
    [SerializeField] private ObjectPool<Projectile> missilePool;
    [SerializeField] private AmmoCounter ammoCounter;
    [SerializeField] private GunBarrelRotator gunBarrelRotator;
    [SerializeField] private PlayerStructure playerStructure;
    [SerializeField] private int speed;
    [SerializeField] private ExplosionStats explosionStats;

    private Action<IAttacker> deregister;

    public Vector3 Position { get => transform.position; }
    public bool CanAttack { get => ammoCounter.HasAmmo; }

    public void Attack(Vector3 target)
    {
        Projectile missile = missilePool.Pull();
        missile.Setup(speed, 0, explosionStats);

        if (ammoCounter.HasAmmo)
        {
            missile.Launch(transform.position, target);
            ammoCounter.SpentAmmo();
            gunBarrelRotator.RotateBarrel(target);
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
