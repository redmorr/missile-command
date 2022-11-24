using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : Destructible
{
    [SerializeField] private Explosion ExplosionPrefab;

    public override void Die()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
