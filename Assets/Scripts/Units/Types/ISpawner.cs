using UnityEngine;

public interface ISpawner
{
    public IAutoAttacker Spawn();
    public Vector3 Position { get; }
}
