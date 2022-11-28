using UnityEngine;

public interface ISpawner
{
    public void InitSpawner(System.Action<ISpawner> action);
    public IAutoAttacker Spawn();
    public Vector3 Position { get; }
}
