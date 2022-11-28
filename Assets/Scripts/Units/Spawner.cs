using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public UnityAction<Spawner> OnBeingDestroyed;

    [SerializeField] private float Frequency;
    [SerializeField] private int PointsForBeingDestroyed;
    [SerializeField] private int Speed;
    [SerializeField] private ExplosionStats ExplosionStats;

    public ICommandSpawns Commander { get; set; }

    public bool CanFire { get => true; }
    public Vector3 Position { get => transform.position; }

    private Launcher launcher;
    private Pool<Autonomous> autonomousPool;

    private void Awake()
    {
        autonomousPool = FindObjectOfType<Pool<Autonomous>>();
        launcher = GetComponent<Launcher>();
    }

    public void Spawn()
    {
        Autonomous autonomous = autonomousPool.Pull();

        Missile missile = autonomous.GetComponent<Missile>();

        missile.Speed = Speed;
        missile.PointsForBeingDestroyed = PointsForBeingDestroyed;
        missile.ExplosionStats = ExplosionStats;

        autonomous.Frequency = Frequency;
        autonomous.Speed = Speed;
        autonomous.PointsForBeingDestroyed = PointsForBeingDestroyed;
        autonomous.ExplosionStats = ExplosionStats;

        launcher.Launch(missile, transform.position, transform.position + Vector3.right * 50f);
    }

    private void OnDisable()
    {
        OnBeingDestroyed?.Invoke(this);
    }
}
