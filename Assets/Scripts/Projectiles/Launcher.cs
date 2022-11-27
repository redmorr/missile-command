using UnityEngine;
using UnityEngine.Events;

public class Launcher : MonoBehaviour
{
    [SerializeField] private ExplosionStats MissileExplostionStats;
    [SerializeField] private int PointsOnMissileDestroyed;

    public UnityAction<Vector3> OnLaunch;

    private IProjectileProvider projectileProvider;

    private void Awake()
    {
        projectileProvider = GetComponent<IProjectileProvider>();
    }

    public void Launch(Vector3 from, Vector3 to)
    {
        IProjectile projectile = projectileProvider.GetProjectile();
        Vector3 directionToTarget = to - from;
        projectile.Setup(from, to);
        OnLaunch?.Invoke(to);
    }
}
