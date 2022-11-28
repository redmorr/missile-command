using UnityEngine;

public interface IAttacker
{
    public void InitPoolable(System.Action<IAttacker> action);
    public void Attack(Vector3 target);

    public Vector3 Position { get; }
}
