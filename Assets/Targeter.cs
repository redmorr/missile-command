using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private Transform Muzzle;

    public Vector3 CalculateTarget(Vector3 target)
    {
        target.y = Mathf.Max(target.y, Muzzle.position.y);
        return target;
    }
}
