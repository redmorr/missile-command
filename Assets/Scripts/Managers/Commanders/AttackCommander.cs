using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackCommander : MonoBehaviour, IOrderUnitAttack
{
    public List<IAttacker> units;

    private void Awake()
    {
        units = GetComponentsInChildren<IAttacker>().ToList();

        foreach (IAttacker unit in units)
        {
            unit.InitPoolable(Deregister);
        }
    }

    public void OrderAttackRandom(Vector3 target)
    {
        if (GetRandomUnit(out IAttacker launcher))
            launcher.Attack(target);
    }

    public void OrderAttack(Vector3 target)
    {
        if (GetUnitClosestToTarget(out IAttacker launcher, target))
            launcher.Attack(target);
    }

    public void Register(IAttacker newUnit)
    {
        units.Add(newUnit);
        newUnit.InitPoolable(Deregister);
    }

    private void Deregister(IAttacker unit)
    {
        units.Remove(unit);
    }

    private bool GetRandomUnit(out IAttacker unit)
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

    private bool GetUnitClosestToTarget(out IAttacker closestUnit, Vector3 target)
    {
        closestUnit = null;
        float minDistnace = Mathf.Infinity;

        foreach (IAttacker unit in units)
        {
            float distance = Vector2.Distance(target, unit.Position);
            if (distance < minDistnace)
            {
                minDistnace = distance;
                closestUnit = unit;
            }
        }
        return closestUnit != null;
    }
}
