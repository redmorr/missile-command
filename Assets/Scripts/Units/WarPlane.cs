using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarPlane : MonoBehaviour, IDestructible
{
    public int Points { get => 1000; }

    public void Die()
    {
        Destroy(gameObject);
    }
}
