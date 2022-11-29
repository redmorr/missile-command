using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyInput : MonoBehaviour
{
    private IOrderUnitAttack attackerCommander;
    private IOrderUnitSpawn spawnerCommander;
    private TargetManager targetManager;
    private Coroutine attackRoutine;
    private Coroutine spawnRoutine;

    public float attackFrequency;
    public int attackNumber;
    public float spawnFrequency;
    public int spawnNumber;

    public UnityAction OnAttackingFinished;

    private void Awake()
    {
        attackerCommander = GetComponent<IOrderUnitAttack>();
        spawnerCommander = GetComponent<IOrderUnitSpawn>();
        targetManager = FindObjectOfType<TargetManager>();
    }

    private IEnumerator Attack()
    {
        while (attackNumber > 0)
        {
            if (targetManager.GetRandomTargetablePosition(out Vector3 pos))
            {
                attackerCommander.OrderAttackRandom(pos);
            }
            yield return new WaitForSeconds(attackFrequency);
            attackNumber--;
        }

        if (attackNumber == 0 && spawnNumber == 0)
        {
            OnAttackingFinished?.Invoke();
        }
    }

    private IEnumerator Spawn()
    {
        while (spawnNumber > 0)
        {
            if (targetManager.GetRandomTargetablePosition(out Vector3 _))
            {
                spawnerCommander.OrderSpawn();
            }
            yield return new WaitForSeconds(spawnFrequency);
            spawnNumber--;
        }

        if (attackNumber == 0 && spawnNumber == 0)
        {
            OnAttackingFinished?.Invoke();
        }
    }

    private void OnDisable()
    {
        StopCoroutine(attackRoutine);
        StopCoroutine(spawnRoutine);
    }

    public void BeginAttacking(SpawnStats spawnStats)
    {
        if (attackRoutine != null)
            StopCoroutine(attackRoutine);

        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        attackFrequency = spawnStats.AttackRatePerSecond;
        attackNumber = spawnStats.AttackNumber;
        spawnFrequency = spawnStats.SpawnRatePerSecond;
        spawnNumber = spawnStats.SpawnNumber;

        attackRoutine = StartCoroutine(Attack());
        spawnRoutine = StartCoroutine(Spawn());
    }
}
