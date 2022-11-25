using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyInput : MonoBehaviour
{
    [SerializeField] private List<Targetable> playersUnits;
    [SerializeField] private ICommander commander;

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
}
