using System;
using UnityEngine;

public class SkyMissileLauncher : MonoBehaviour, IAttacker
{
    [SerializeField] private ObjectPool<Missile> missilePool;
    [SerializeField] private ProjectileData projectileData;

    private IOrderUnitAttack attackCommander;

    public Vector3 Position { get => transform.position; }
    public bool CanAttack => true;

    private void Awake()
    {
        attackCommander = GetComponentInParent<IOrderUnitAttack>();
    }

    public void Attack(Vector3 target)
    {
        Missile missile = missilePool.Pull();
        missile.Launch(transform.position, target, projectileData);
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
