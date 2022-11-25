using UnityEngine;

public interface ILaunchMissile
{
    public void Launch(Vector3 target);

    public bool CanFire { get; }

    public Vector3 Position { get; }
}
