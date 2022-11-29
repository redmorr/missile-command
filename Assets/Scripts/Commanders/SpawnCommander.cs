using System.Collections.Generic;
using UnityEngine;

public class SpawnCommander : MonoBehaviour, IOrderUnitSpawn
{
    private List<ISpawner> spawners = new List<ISpawner>();
    private List<IAutoAttacker> autoAttackers = new List<IAutoAttacker>();

    public void Deregister(ISpawner unit)
    {
        spawners.Remove(unit);
    }

    public void Register(ISpawner unit)
    {
        spawners.Add(unit);
    }

    public void OrderSpawn()
    {
        if (GetRandomUnit(out ISpawner spawner))
        {
            IAutoAttacker autoAttacker = spawner.Spawn();
            autoAttacker.SetupActionOnDeath(DeregisterAuto);
            autoAttackers.Add(autoAttacker);
        }
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
