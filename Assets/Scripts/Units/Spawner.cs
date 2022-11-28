using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour, ISpawner
{
    public UnityAction<Spawner> OnBeingDestroyed;

    [SerializeField] private ObjectPool<Autonomous> autonomousPool;
    [SerializeField] private float Frequency;
    [SerializeField] private int PointsForBeingDestroyed;
    [SerializeField] private int Speed;
    [SerializeField] private ExplosionStats ExplosionStats;

    public bool CanFire { get => true; }
    public Vector3 Position { get => transform.position; }

    private Action<ISpawner> deregister;
    private Launcher launcher;

    private void Awake()
    {
        launcher = GetComponent<Launcher>();
    }

    public IAutonomous Spawn()
    {
        Autonomous autonomous = autonomousPool.Pull();

        Missile missile = autonomous.GetComponent<Missile>();

        missile.Setup(Speed, PointsForBeingDestroyed, ExplosionStats);

        autonomous.Setup(Frequency, Speed, PointsForBeingDestroyed, ExplosionStats);

        launcher.Launch(missile, transform.position, transform.position + Vector3.right * 50f);

        return autonomous;
    }

    private void OnDisable()
    {
        deregister?.Invoke(this);
    }

    public void InitPoolable(Action<ISpawner> action)
    {
        deregister = action;
    }

}
