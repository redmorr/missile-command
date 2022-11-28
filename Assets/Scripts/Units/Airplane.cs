using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Airplane : MonoBehaviour, IPoolable<Airplane>, IAutoAttacker
{
    private Action<Airplane> returnToPool;

    private float attackFrequency;
    private int pointsForBeingDestroyed;
    private float speed;
    private ExplosionStats explosionStats;

    private Action<IAutoAttacker> deregister;
    public Vector3 Position { get => transform.position; }

    private Launcher launcher;
    private ObjectPool<Missile> missilePool;
    private TargetManager targetManager;
    private Coroutine attackRoutine;

    public void Setup(float attackFrequency, float speed, int pointsForBeingDestroyed, ExplosionStats explosionStats)
    {
        this.attackFrequency = attackFrequency;
        this.speed = speed;
        this.pointsForBeingDestroyed = pointsForBeingDestroyed;
        this.explosionStats = explosionStats;
    }

    private void Awake()
    {
        targetManager = FindObjectOfType<TargetManager>();
        missilePool = FindObjectOfType<ObjectPool<Missile>>();
        launcher = GetComponent<Launcher>();
    }

    private void OnEnable()
    {
        attackRoutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackFrequency);

            if (targetManager.GetRandomTargetablePosition(out Vector3 pos))
            {
                Missile missile = missilePool.Pull();
                missile.Setup(speed, pointsForBeingDestroyed, explosionStats);
                launcher.Launch(missile, transform.position, pos);
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(attackRoutine);
        deregister?.Invoke(this);
        returnToPool?.Invoke(this);
    }

    public void InitPoolable(Action<Airplane> action) => returnToPool = action;

    public void SetupAutoAttacker(Action<IAutoAttacker> action) => deregister = action;

    public void ReturnToPool() => returnToPool?.Invoke(this);

}
