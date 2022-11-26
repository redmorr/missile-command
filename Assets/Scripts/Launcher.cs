using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Launcher : MonoBehaviour
{
    [SerializeField] private float MissileSpeed = 5f;
    [SerializeField] private ExplosionStats MissileExplostionStats;
    [SerializeField] private int PointsOnMissileDestroyed;

    public UnityAction<Vector3> OnLaunch;

    private MissilePool missilePool;

    private void Awake()
    {
        missilePool = FindObjectOfType<MissilePool>();
    }

    public void Launch(Vector3 from, Vector3 to)
    {
        Missile missile = missilePool.Pull;
        
        missile.Setup(from,
                      to,
                      MissileSpeed,
                      PointsOnMissileDestroyed,
                      MissileExplostionStats);
        OnLaunch?.Invoke(to);
    }
}
