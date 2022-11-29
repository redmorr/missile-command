using UnityEngine;

public interface IAttacker
{
    public void Attack(Vector3 target);
    public Vector3 Position { get; }
    public bool CanAttack { get; }
    public void Disable();
    public void Enable();
}
