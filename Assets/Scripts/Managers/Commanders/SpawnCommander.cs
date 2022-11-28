using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnCommander : MonoBehaviour, IOrderUnitSpawn
{
    private List<ISpawner> spawners;
    private List<IAutoAttacker> autoAttackers = new List<IAutoAttacker>();

    private void Awake()
    {
        spawners = GetComponentsInChildren<ISpawner>().ToList();

        foreach (ISpawner unit in spawners)
        {
            unit.InitSpawner(Deregister);
        }
    }

    public void OrderSpawn()
    {
        if (GetRandomUnit(out ISpawner spawner))
        {
            IAutoAttacker autoAttacker = spawner.Spawn();
            autoAttacker.SetupAutoAttacker(DeregisterAuto);
            autoAttackers.Add(autoAttacker);
        }
    }

    public void Register(ISpawner newUnit)
    {
        spawners.Add(newUnit);
        newUnit.InitSpawner(Deregister);
    }

    private void Deregister(ISpawner unit)
    {
        spawners.Remove(unit);
    }

    private void DeregisterAuto(IAutoAttacker unit)
    {
        autoAttackers.Remove(unit);
    }

    private bool GetRandomUnit(out ISpawner spawner)
    {
        spawner = null;

        if (spawners.Count > 0)
        {
            int randomIndex = Random.Range(0, spawners.Count);
            spawner = spawners[randomIndex];
            return true;
        }

        return false;
    }

}
