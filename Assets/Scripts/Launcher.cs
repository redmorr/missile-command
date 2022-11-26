using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Launcher : MonoBehaviour
{
    [SerializeField] private float MissileSpeed = 5f;
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
        Vector3 directionToTarget = to - from;
        missile.Setup(from, Quaternion.LookRotation(Vector3.forward, directionToTarget), from, to, MissileSpeed, PointsOnMissileDestroyed);
        OnLaunch?.Invoke(to);
    }
}
