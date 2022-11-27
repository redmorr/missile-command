using UnityEngine;

public class ProjectileProvider<T> : MonoBehaviour, IProjectileProvider where T : MonoBehaviour, IProjectile, IPoolable<T>
{
    private Pool<T> projectilePool;

    private void Awake()
    {
        projectilePool = FindObjectOfType<Pool<T>>();
    }

    public IProjectile GetProjectile()
    {
        return projectilePool.Pull();
    }
}
