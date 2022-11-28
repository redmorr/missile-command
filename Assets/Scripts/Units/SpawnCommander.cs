using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnCommander : MonoBehaviour, ICommandSpawns
{
    public List<Spawner> units;
    public List<Autonomous> autonomousUnits;

    private void Awake()
    {
        units = GetComponentsInChildren<Spawner>().ToList();

        foreach (Spawner unit in units)
        {
            unit.Commander = this;
            unit.OnBeingDestroyed += Unregister;
        }
    }

    public void Spawn()
    {
        if (GetRandomUnit(out Spawner spawner))
            spawner.Spawn();
    }

    public void Register(Spawner newUnit)
    {
        units.Add(newUnit);
        newUnit.Commander = this;
        newUnit.OnBeingDestroyed += Unregister;
    }

    private void Unregister(Spawner unit)
    {
        units.Remove(unit);
        unit.OnBeingDestroyed -= Unregister;
    }

    private bool GetRandomUnit(out Spawner unit)
    {
        unit = null;
        var randomIndex = Random.Range(0, units.Count);

        if (units[randomIndex])
        {
            unit = units[randomIndex];
            return true;
        }

        return false;
    }

    private bool GetUnitClosestToTarget(out Spawner closestUnit, Vector3 target)
    {
        closestUnit = null;
        float minDistnace = Mathf.Infinity;

        foreach (Spawner unit in units)
        {
            if (unit.CanFire)
            {
                float distance = Vector2.Distance(target, unit.Position);
                if (distance < minDistnace)
                {
                    minDistnace = distance;
                    closestUnit = unit;
                }
            }
        }

        return closestUnit != null;
    }


}
