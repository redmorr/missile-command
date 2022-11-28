using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAutonomous
{
    public void InitDeregistration(System.Action<IAutonomous> action);
}
