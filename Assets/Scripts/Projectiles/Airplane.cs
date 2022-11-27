using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : Missile, IPoolable<Airplane>
{
    new protected Action<Airplane> returnToPool;

    public void InitPoolable(Action<Airplane> action)
    {
        returnToPool = action;
    }

}
