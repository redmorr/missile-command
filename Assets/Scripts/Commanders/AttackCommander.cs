using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackCommander : MonoBehaviour, IOrderUnitAttack
{
    private List<IAttacker> attackers = new List<IAttacker>();

    public void Deregister(IAttacker unit)
    {
        attackers.Remove(unit);
    }

    public void Register(IAttacker unit)
    {
        attackers.Add(unit);
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

    private bool GetRandomUnit(out IAttacker unit)
    {
        unit = null;
        var randomIndex = Random.Range(0, attackers.Count);

        if (attackers.Count > 0)
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

        foreach (IAttacker attacker in attackers)
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
