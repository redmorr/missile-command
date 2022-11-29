using System;
using UnityEngine;

public class SkyMissileLauncher : MonoBehaviour, IAttacker
{
    [SerializeField] private ObjectPool<Projectile> missilePool;
    [SerializeField] private int pointsForDestroyingMissile;
    [SerializeField] private int speed;
    [SerializeField] private ExplosionStats explosionStats;

    private Action<IAttacker> deregister;

    public Vector3 Position { get => transform.position; }
    public bool CanAttack => true;

    public void Attack(Vector3 target)
    {
        Projectile missile = missilePool.Pull();
        missile.Setup(speed, pointsForDestroyingMissile, explosionStats);
        missile.Launch(transform.position, target);
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
