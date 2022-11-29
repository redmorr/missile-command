using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetManager : Singleton<TargetManager>
{
    public List<PlayerStructure> initalPlayerStructures;
    public List<PlayerStructure> activePlayerStructures;

    public int PlayerStructuresNumber { get => activePlayerStructures.Count; }

    protected override void Awake()
    {
        base.Awake();

        initalPlayerStructures = FindObjectsOfType<PlayerStructure>().ToList();
        activePlayerStructures = FindObjectsOfType<PlayerStructure>().ToList();

        foreach (PlayerStructure targetable in activePlayerStructures)
            targetable.OnBeingDestroyed += RemoveFromList;
    }

    public bool GetRandomTargetablePosition(out Vector3 targetPosition)
    {
        targetPosition = Vector3.zero;

        if (activePlayerStructures.Count <= 0)
            return false;

        var i = Random.Range(0, activePlayerStructures.Count);

        if (activePlayerStructures[i])
        {
            targetPosition = activePlayerStructures[i].Position;
            return true;
        }
        else
        {
            Debug.LogWarning("Could not get a targetable position");
            return false;
        }
    }

    public void ReactivateAll()
    {
        activePlayerStructures = new List<PlayerStructure>(initalPlayerStructures);
        foreach (var item in activePlayerStructures)
        {
            item.gameObject.SetActive(true);
        }
    }

    private void RemoveFromList(PlayerStructure targetable)
    {
        activePlayerStructures.Remove(targetable);
        targetable.OnBeingDestroyed -= RemoveFromList;
    }
}
