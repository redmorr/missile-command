using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : Singleton<ExplosionPool>
{
    [SerializeField] private int InitialPoolSize;
    [SerializeField] private Explosion ExplosionPrefab;

    private Pool<Explosion> explosionPool;

    public Explosion Pull { get => explosionPool.Get(); }

    protected override void Awake()
    {
        base.Awake();
        explosionPool = new Pool<Explosion>(ExplosionPrefab, InitialPoolSize);
    }
}
