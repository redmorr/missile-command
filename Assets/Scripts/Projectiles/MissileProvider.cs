using UnityEngine;

public class MissileProvider : MonoBehaviour, IProjectileProvider
{
    [SerializeField] private int PointsForBeingDestroyed;
    [SerializeField] private int Speed;
    [SerializeField] private ExplosionStats ExplosionStats;

    private Pool<Missile> projectilePool;

    private void Awake()
    {
        projectilePool = FindObjectOfType<Pool<Missile>>();
    }

    public IProjectile GetProjectile()
    {
        Missile missile = projectilePool.Pull();
        missile.Speed = Speed;
        missile.ExplosionStats = ExplosionStats;
        missile.PointsForBeingDestroyed = PointsForBeingDestroyed;
        return missile;
    }
}
