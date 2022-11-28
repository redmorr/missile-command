using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnCommander : MonoBehaviour, ICommandSpawns
{
    public List<ISpawner> units;
    public List<Autonomous> autonomousUnits;

    private void Awake()
    {
        units = GetComponentsInChildren<ISpawner>().ToList();

        foreach (Spawner unit in units)
        {
            unit.OnBeingDestroyed += Deregister;
        }
    }

    public void Spawn()
    {
        if (GetRandomUnit(out ISpawner spawner))
            spawner.Spawn();
    }

    public void Register(ISpawner newUnit)
    {
        units.Add(newUnit);
        newUnit.InitPoolable(Deregister);
    }

    private void Deregister(ISpawner unit)
    {
        units.Remove(unit);
    }

    private bool GetRandomUnit(out ISpawner unit)
    {
        unit = null;
        var randomIndex = Random.Range(0, units.Count);

        if (units.Count > 0)
        {
            unit = units[randomIndex];
            return true;
        }

        return false;
    }

}
