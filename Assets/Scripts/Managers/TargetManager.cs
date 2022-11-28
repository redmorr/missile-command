using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetManager : Singleton<TargetManager>
{
    private List<Targetable> playersUnits;

    protected override void Awake()
    {
        base.Awake();
        playersUnits = FindObjectsOfType<Targetable>().ToList();

        foreach (Targetable targetable in playersUnits)
        {
            targetable.OnBeingDestroyed += RemoveFromList;
        }
    }

    public bool GetRandomTargetablePosition(out Vector3 targetPosition)
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

    private void RemoveFromList(Targetable targetable)
    {
        playersUnits.Remove(targetable);
        targetable.OnBeingDestroyed -= RemoveFromList;
    }
}
