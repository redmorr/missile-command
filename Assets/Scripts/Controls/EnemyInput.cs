using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyInput : MonoBehaviour
{
    [SerializeField] private float AttackFrequency = 2f;
    [SerializeField] private float SpawnFrequency = 3f;

    private ICommandAttacks attackerCommander;
    private ICommandSpawns spawnerCommander;
    private TargetManager targetManager;
    private Coroutine coroutine;

    private void Awake()
    {
        attackerCommander = GetComponent<ICommandAttacks>();
        spawnerCommander = GetComponent<ICommandSpawns>();
        targetManager = FindObjectOfType<TargetManager>();
    }

    private void Start()
    {
        coroutine = StartCoroutine(Attack());
        coroutine = StartCoroutine(Spawn());
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
            yield return new WaitForSeconds(AttackFrequency);
            if (targetManager.GetRandomTargetablePosition(out Vector3 _))
            {
                spawnerCommander.Spawn();
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
}
