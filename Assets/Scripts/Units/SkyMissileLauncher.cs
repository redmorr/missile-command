using System;
using UnityEngine;

public class SkyMissileLauncher : MonoBehaviour, IAttacker
{
    [SerializeField] private ObjectPool<Projectile> missilePool;
    [SerializeField] private int pointsForDestroyingMissile;
    [SerializeField] private int speed;
    [SerializeField] private ExplosionStats explosionStats;

    private IOrderUnitAttack attackCommander;

    public Vector3 Position { get => transform.position; }
    public bool CanAttack => true;

    private void Awake()
    {
        attackCommander = GetComponentInParent<IOrderUnitAttack>();
    }

    public void Attack(Vector3 target)
    {
        Projectile missile = missilePool.Pull();
        missile.Setup(speed, pointsForDestroyingMissile, explosionStats);
        missile.Launch(transform.position, target);
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
