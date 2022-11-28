using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

public class Autonomous : MonoBehaviour, IPoolable<Autonomous>
{
    protected Action<Autonomous> returnToPool;

    private float Frequency;
    private int PointsForBeingDestroyed;
    private float Speed;
    private ExplosionStats ExplosionStats;

    public UnityAction<Attacker> OnBeingDestroyed;

    public ICommandSpawns Commander { get; set; }
    public bool CanFire { get => true; }
    public Vector3 Position { get => transform.position; }

    private Launcher launcher;
    private Pool<Missile> missilePool;
    private TargetManager targetManager;
    private Coroutine coroutine;

    private void Awake()
    {
        targetManager = FindObjectOfType<TargetManager>();
        missilePool = FindObjectOfType<Pool<Missile>>();
        launcher = GetComponent<Launcher>();
    }

    private void OnEnable()
    {
        coroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Frequency);

            if (targetManager.GetRandomTargetablePosition(out Vector3 pos))
            {
                Missile missile = missilePool.Pull();
                missile.Setup(Speed, PointsForBeingDestroyed, ExplosionStats);
                launcher.Launch(missile, transform.position, pos);
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }

    public void InitPoolable(Action<Autonomous> action)
    {
        returnToPool = action;
    }

    public void ReturnToPool()
    {
        returnToPool?.Invoke(this);
    }

    public void Setup(float frequency, float speed, int pointsForBeingDestroyed, ExplosionStats explosionStats)
    {
        Frequency = frequency;
        Speed = speed;
        PointsForBeingDestroyed = pointsForBeingDestroyed;
        ExplosionStats = explosionStats;
    }
}
