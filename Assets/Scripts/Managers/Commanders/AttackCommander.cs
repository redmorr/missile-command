using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackCommander : MonoBehaviour, IOrderUnitAttack
{
    private List<IAttacker> attackers;
    private List<IAttacker> activeAttackers;

    private void Awake()
    {
        attackers = GetComponentsInChildren<IAttacker>().ToList();
        activeAttackers = attackers;

        foreach (IAttacker attacker in attackers)
        {
            attacker.SetupAttacker(Deregister);
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

    private void Deregister(IAttacker unit)
    {
        activeAttackers.Remove(unit);
    }

    private bool GetRandomUnit(out IAttacker unit)
    {
        unit = null;
        var randomIndex = Random.Range(0, activeAttackers.Count);

        if (activeAttackers.Count > 0)
        {
            unit = attackers[randomIndex];
            return true;
        }

        return false;
    }

    private bool GetUnitClosestToTarget(out IAttacker closestAttacker, Vector3 target)
    {
        closestAttacker = null;
        float minDistnace = Mathf.Infinity;

        foreach (IAttacker attacker in activeAttackers)
        {
            float distance = Vector2.Distance(target, attacker.Position);
            if (distance < minDistnace)
            {
                minDistnace = distance;
                closestAttacker = attacker;
            }
        }
        return closestAttacker != null;
    }
}
