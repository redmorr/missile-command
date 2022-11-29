using System;
using UnityEngine;

public class AirplaneSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private ObjectPool<Airplane> airplanePool;
    [SerializeField] private float frequency;
    [SerializeField] private int pointsForBeingDestroyed;
    [SerializeField] private int speed;
    [SerializeField] private ExplosionStats explosionStats;

    public Vector3 Position { get => transform.position; }

    private Action<ISpawner> deregister;

    public IAutoAttacker Spawn()
    {
        Airplane autonomous = airplanePool.Pull();
        Projectile projectile = autonomous.GetComponent<Projectile>();
        projectile.Setup(speed, pointsForBeingDestroyed, explosionStats);
        autonomous.Setup(frequency, speed, pointsForBeingDestroyed, explosionStats);
        projectile.Launch(transform.position, transform.position + Vector3.right * 50f);
        return autonomous;
    }

    public void InitSpawner(Action<ISpawner> action)
    {
        deregister = action;
    }

    private void OnDisable()
    {
        deregister?.Invoke(this);
    }
}
