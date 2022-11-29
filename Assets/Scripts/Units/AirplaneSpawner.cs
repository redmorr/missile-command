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
        Projectile missile = autonomous.GetComponent<Projectile>();
        missile.Setup(speed, pointsForBeingDestroyed, explosionStats);
        autonomous.Setup(frequency, speed, pointsForBeingDestroyed, explosionStats);
        missile.Launch(transform.position, transform.position + Vector3.right * 50f);
        return autonomous;
    }

    private void OnDisable()
    {
        deregister?.Invoke(this);
    }

    public void InitSpawner(Action<ISpawner> action)
    {
        deregister = action;
    }
}
