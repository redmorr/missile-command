using UnityEngine;
using UnityEngine.Events;

public class Launcher : MonoBehaviour
{
    [SerializeField] private ExplosionStats MissileExplostionStats;
    [SerializeField] private int PointsOnMissileDestroyed;

    public UnityAction<Vector3> OnLaunch;

    private AmmoProvider ammoProvider;

    private void Awake()
    {
        ammoProvider = GetComponent<AmmoProvider>();
    }

    public void Launch(Vector3 from, Vector3 to)
    {
        IProjectile projectile = ammoProvider.GetProjectile();
        Vector3 directionToTarget = to - from;
        projectile.Setup(from, to);
        OnLaunch?.Invoke(to);
    }
}
