using System;
using UnityEngine;

[RequireComponent(typeof(AmmoCounter))]
[RequireComponent(typeof(GunBarrelRotator))]
[RequireComponent(typeof(PlayerStructure))]
public class GroundMissileLauncher : MonoBehaviour, IAttacker
{
    [SerializeField] private ObjectPool<Missile> missilePool;
    [SerializeField] private Transform muzzle;
    [SerializeField] private ProjectileData projectileData;

    private AmmoCounter ammoCounter;
    private GunBarrelRotator gunBarrelRotator;
    private IOrderUnitAttack attackCommander;

    public Vector3 Position { get => transform.position; }
    public bool CanAttack { get => ammoCounter.HasAmmo; }

    private void Awake()
    {
        attackCommander = GetComponentInParent<IOrderUnitAttack>();
        ammoCounter = GetComponent<AmmoCounter>();
        gunBarrelRotator = GetComponent<GunBarrelRotator>();
    }

    public void Attack(Vector3 target)
    {
        if (ammoCounter.HasAmmo)
        {
            gunBarrelRotator.RotateBarrel(target);
            Missile missile = missilePool.Pull();
            missile.Launch(muzzle.position, target, projectileData);
            ammoCounter.SpentAmmo();

            if (!ammoCounter.HasAmmo)
                attackCommander.Deregister(this);
        }
    }

    private void OnEnable()
    {
        attackCommander.Register(this);
    }

    private void OnDisable()
    {
        attackCommander.Deregister(this);
    }

}
