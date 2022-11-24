using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarPlane : MonoBehaviour, IDestructible
{
    public void Die()
    {
        Destroy(gameObject);
    }
}
