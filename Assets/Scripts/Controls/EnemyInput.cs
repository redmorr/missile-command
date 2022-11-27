using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyInput : MonoBehaviour
{
    [SerializeField] private float frequency = 2f;
    [SerializeField] private List<Targetable> playersUnits;
    [SerializeField] private ICommander commander;

    private Coroutine coroutine;

    private void Awake()
    {
        commander = GetComponent<ICommander>();
        playersUnits = FindObjectsOfType<Targetable>().ToList();

        foreach (Targetable targetable in playersUnits)
        {
            targetable.OnBeingDestroyed += RemoveFromList;
        }
    }

    private void RemoveFromList(Targetable targetable)
    {
        playersUnits.Remove(targetable);
        targetable.OnBeingDestroyed -= RemoveFromList;
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
            if (GetRandomTargetablePosition(out Vector3 pos))
            {
                commander.OrderAttackRandom(pos);
            }
        }
    }

    private bool GetRandomTargetablePosition(out Vector3 targetPosition)
    {
        targetPosition = Vector3.zero;

        if (playersUnits.Count <= 0)
            return false;

        var i = Random.Range(0, playersUnits.Count);

        if (playersUnits[i])
        {
            targetPosition = playersUnits[i].Position;
            return true;
        }
        else
        {
            Debug.LogWarning("Could not get a targetable position");
            return false;
        }

    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
}
