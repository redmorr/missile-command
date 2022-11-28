using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface ISpawner
{
    public void InitPoolable(System.Action<ISpawner> action);
    public IAutonomous Spawn();

    public bool CanFire { get; }
    public Vector3 Position { get; }
}
