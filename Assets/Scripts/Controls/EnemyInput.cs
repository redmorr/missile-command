using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyInput : MonoBehaviour
{
    [SerializeField] private float frequency = 2f;
    
    private ICommander commander;
    private TargetManager targetManager;
    private Coroutine coroutine;

    private void Awake()
    {
        commander = GetComponent<ICommander>();
        targetManager = FindObjectOfType<TargetManager>();
    }

    private void Start()
    {
        coroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(frequency);
            if (targetManager.GetRandomTargetablePosition(out Vector3 pos))
            {
                commander.OrderAttackRandom(pos);
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
}
