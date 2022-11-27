using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneProvider : MonoBehaviour, IProjectileProvider
{
    [SerializeField] private int PointsForBeingDestroyed;
    [SerializeField] private int Speed;
    [SerializeField] private ExplosionStats ExplosionStats;

    private Pool<Airplane> projectilePool;
    private SkyLauncher skyLauncher;

    private void Awake()
    {
        projectilePool = FindObjectOfType<Pool<Airplane>>();
        skyLauncher = GetComponent<SkyLauncher>();
    }

    public IProjectile GetProjectile()
    {
        Airplane missile = projectilePool.Pull();

        ILaunchMissile newSkyLauncher = missile.GetComponent<ILaunchMissile>();
        skyLauncher.EnemyCommander.Register(newSkyLauncher);


        missile.Speed = Speed;
        missile.ExplosionStats = ExplosionStats;
        missile.PointsForBeingDestroyed = PointsForBeingDestroyed;
        return missile;
    }
}
