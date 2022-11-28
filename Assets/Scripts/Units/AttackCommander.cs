using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackCommander : MonoBehaviour, ICommandAttacks
{
    public List<Attacker> units;

    private void Awake()
    {
        units = GetComponentsInChildren<Attacker>().ToList();

        foreach (Attacker unit in units)
        {
            unit.Commander = this;
            unit.OnBeingDestroyed += Unregister;
        }
    }

    public void OrderAttackRandom(Vector3 target)
    {
        if (GetRandomUnit(out Attacker launcher))
            launcher.Launch(target);
    }

    public void OrderAttack(Vector3 target)
    {
        if (GetUnitClosestToTarget(out Attacker launcher, target))
            launcher.Launch(target);
    }

    public void Register(Attacker newUnit)
    {
        units.Add(newUnit);
        newUnit.Commander = this;
        newUnit.OnBeingDestroyed += Unregister;
    }

    private void Unregister(Attacker unit)
    {
        units.Remove(unit);
        unit.OnBeingDestroyed -= Unregister;
    }

    private bool GetRandomUnit(out Attacker unit)
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

    private bool GetUnitClosestToTarget(out Attacker closestUnit, Vector3 target)
    {
        closestUnit = null;
        float minDistnace = Mathf.Infinity;

        foreach (Attacker unit in units)
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
