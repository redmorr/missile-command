using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Commander : MonoBehaviour, ICommander
{
    public List<Unit> units;

    private void Awake()
    {
        units = GetComponentsInChildren<Unit>().ToList();

        foreach (Unit unit in units)
        {
            unit.Commander = this;
            unit.OnBeingDestroyed += Unregister;
        }
    }

    public void OrderAttackRandom(Vector3 target)
    {
        if (GetRandomUnit(out Unit launcher))
            launcher.Launch(target);
    }

    public void OrderAttack(Vector3 target)
    {
        if (GetUnitClosestToTarget(out Unit launcher, target))
            launcher.Launch(target);
    }

    public void Register(Unit newUnit)
    {
        units.Add(newUnit);
        newUnit.Commander = this;
        newUnit.OnBeingDestroyed += Unregister;
    }

    private void Unregister(Unit unit)
    {
        units.Remove(unit);
        unit.OnBeingDestroyed -= Unregister;
    }

    private bool GetRandomUnit(out Unit unit)
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

    private bool GetUnitClosestToTarget(out Unit closestUnit, Vector3 target)
    {
        closestUnit = null;
        float minDistnace = Mathf.Infinity;

        foreach (Unit unit in units)
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
