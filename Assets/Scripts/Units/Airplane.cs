using System;
using System.Collections;
using UnityEngine;

public class Airplane : MonoBehaviour, IPoolable<Airplane>, IAutoAttacker
{
    private Action<Airplane> returnToPool;
    private float attackFrequency;
    private int pointsForBeingDestroyed;
    private float speed;
    private ExplosionStats explosionStats;
    private ObjectPool<Projectile> missilePool;
    private TargetManager targetManager;
    private Coroutine attackRoutine;
    
    private Action<IAutoAttacker> deregister;

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
        missilePool = FindObjectOfType<ObjectPool<Projectile>>();
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

            if (targetManager.GetRandomTargetablePosition(out Vector3 target))
            {
                Projectile missile = missilePool.Pull();
                missile.Setup(speed, pointsForBeingDestroyed, explosionStats);
                missile.Launch(transform.position, target);
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

    public void SetupActionOnDeath(Action<IAutoAttacker> action) => deregister = action;

    public void ReturnToPool() => returnToPool?.Invoke(this);

}
