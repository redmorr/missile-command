using System;
using System.Collections;
using UnityEngine;

public class Airplane : MonoBehaviour, IPoolable<Airplane>, IAutoAttacker
{
    private float attackFrequency;
    private TargetManager targetManager;
    private Coroutine attackRoutine;
    private ProjectileData projectileData;
    private Action<Airplane> returnToPool;
    private ObjectPool<Missile> missilePool;
    private Action<IAutoAttacker> deregister;

    private void Awake()
    {
        targetManager = FindObjectOfType<TargetManager>();
        missilePool = FindObjectOfType<ObjectPool<Missile>>();
    }

    public void Activate(float attackFrequency, ProjectileData projectileData)
    {
        this.attackFrequency = attackFrequency;
        this.projectileData = projectileData;
        attackRoutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackFrequency);

            if (targetManager.GetRandomTargetablePosition(out Vector3 target))
            {
                Missile missile = missilePool.Pull();
                missile.Launch(transform.position, target, projectileData);
            }
        }
    }

    private void OnDisable()
    {
        if (attackRoutine != null)
            StopCoroutine(attackRoutine);
        deregister?.Invoke(this);
        returnToPool?.Invoke(this);
    }

    public void InitPoolable(Action<Airplane> action) => returnToPool = action;

    public void SetupActionOnDeath(Action<IAutoAttacker> action) => deregister = action;

    public void ReturnToPool() => returnToPool?.Invoke(this);
}
