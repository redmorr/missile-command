using UnityEngine;

public interface IOrderUnitAttack
{
    public void OrderAttack(Vector3 target);
    public void OrderAttackRandom(Vector3 target);
    public void Register(IAttacker attacker);
    public void Deregister(IAttacker attacker);
}
