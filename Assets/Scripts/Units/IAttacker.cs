using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface IAttacker
{
    public void InitPoolable(System.Action<IAttacker> action);
    public void Attack(Vector3 target);

    public bool CanFire { get; }
    public Vector3 Position { get; }
}
