using UnityEngine;

public interface ICommandAttacks
{
    public void OrderAttack(Vector3 target);
    public void OrderAttackRandom(Vector3 target);
}
