using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetManager : Singleton<TargetManager>
{
    private List<PlayerStructure> playerStructures;

    public int PlayerStructuresNumber { get => playerStructures.Count; }

    protected override void Awake()
    {
        base.Awake();

        playerStructures = FindObjectsOfType<PlayerStructure>().ToList();

        foreach (PlayerStructure targetable in playerStructures)
            targetable.OnBeingDestroyed += RemoveFromList;
    }

    public bool GetRandomTargetablePosition(out Vector3 targetPosition)
    {
        targetPosition = Vector3.zero;

        if (playerStructures.Count <= 0)
            return false;

        var i = Random.Range(0, playerStructures.Count);

        if (playerStructures[i])
        {
            targetPosition = playerStructures[i].Position;
            return true;
        }
        else
        {
            Debug.LogWarning("Could not get a targetable position");
            return false;
        }
    }

    private void RemoveFromList(PlayerStructure targetable)
    {
        playerStructures.Remove(targetable);
        targetable.OnBeingDestroyed -= RemoveFromList;
    }
}
