using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class AirplaneProvider : MonoBehaviour, IProjectileProvider
{
    [SerializeField] private int PointsForBeingDestroyed;
    [SerializeField] private int Speed;
    [SerializeField] private ExplosionStats ExplosionStats;

    private Pool<Airplane> projectilePool;
    private Launcher launcher;

    private void Awake()
    {
        projectilePool = FindObjectOfType<Pool<Airplane>>();
        launcher = GetComponent<Launcher>();
    }

    public IProjectile GetProjectile()
    {
        Airplane missile = projectilePool.Pull();

        Unit thisUnit = launcher.GetComponent<Unit>();
        Unit newSkyLauncher = missile.GetComponent<Unit>();
        thisUnit.Commander.Register(newSkyLauncher);

        missile.Speed = Speed;
        missile.ExplosionStats = ExplosionStats;
        missile.PointsForBeingDestroyed = PointsForBeingDestroyed;
        return missile;
    }
}
