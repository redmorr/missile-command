using System;
using UnityEngine;
using UnityEngine.Events;

public class Attacker : MonoBehaviour, IAttacker
{
    [SerializeField] private ObjectPool<Missile> missilePool;
    [SerializeField] private int PointsForBeingDestroyed;
    [SerializeField] private int Speed;
    [SerializeField] private ExplosionStats ExplosionStats;

    public UnityAction<Attacker> OnBeingDestroyed;
    Action<IAttacker> deregister;

    public bool CanFire { get => true; }
    public Vector3 Position { get => transform.position; }

    private Launcher launcher;

    private void Awake()
    {
        launcher = GetComponent<Launcher>();
    }

    public void Attack(Vector3 target)
    {
        Missile missile = missilePool.Pull();
        missile.Setup(Speed, PointsForBeingDestroyed, ExplosionStats);
        launcher.Launch(missile, transform.position, target);
    }

    private void OnDisable()
    {
        deregister?.Invoke(this);
    }

    public void InitPoolable(Action<IAttacker> action)
    {
        deregister = action;
    }
}
