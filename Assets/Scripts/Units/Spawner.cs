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
    private Pool<Airplane> airplanePool;

    private void Awake()
    {
        airplanePool = FindObjectOfType<Pool<Airplane>>();
        launcher = GetComponent<Launcher>();
    }

    public void Spawn()
    {
        Airplane airplane = airplanePool.Pull();

        Autonomous autonomous = airplane.GetComponent<Autonomous>();
        autonomous.Frequency = Frequency;
        autonomous.Speed = Speed;
        autonomous.PointsForBeingDestroyed = PointsForBeingDestroyed;
        autonomous.ExplosionStats = ExplosionStats;

        airplane.Speed = Speed;
        airplane.PointsForBeingDestroyed = PointsForBeingDestroyed;
        airplane.ExplosionStats = ExplosionStats;

        launcher.Launch(airplane, transform.position, transform.position + Vector3.right * 50f);
    }

    private void OnDisable()
    {
        OnBeingDestroyed?.Invoke(this);
    }
}
