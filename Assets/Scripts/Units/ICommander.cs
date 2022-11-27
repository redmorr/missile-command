using UnityEngine;

public interface ICommander
{
    public void OrderAttack(Vector3 target);
    public void OrderAttackRandom(Vector3 target);
}
