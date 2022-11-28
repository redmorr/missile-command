using System.Collections;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    [SerializeField] private float AttackFrequency = 2f;
    [SerializeField] private float SpawnFrequency = 2f;

    private IOrderUnitAttack attackerCommander;
    private IOrderUnitSpawn spawnerCommander;
    private TargetManager targetManager;
    private Coroutine attackRoutine;
    private Coroutine spawnRoutine;

    private void Awake()
    {
        attackerCommander = GetComponent<IOrderUnitAttack>();
        spawnerCommander = GetComponent<IOrderUnitSpawn>();
        targetManager = FindObjectOfType<TargetManager>();
    }

    private void Start()
    {
        attackRoutine = StartCoroutine(Attack());
        spawnRoutine = StartCoroutine(Spawn());
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackFrequency);
            if (targetManager.GetRandomTargetablePosition(out Vector3 pos))
            {
                attackerCommander.OrderAttackRandom(pos);
            }
        }
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnFrequency);
            if (targetManager.GetRandomTargetablePosition(out Vector3 _))
            {
                spawnerCommander.Spawn();
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(attackRoutine);
        StopCoroutine(spawnRoutine);
    }
}
