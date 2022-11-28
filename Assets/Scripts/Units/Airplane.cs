using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Airplane : MonoBehaviour, IPoolable<Airplane>, IDestructible, IPointsOnDestroyed, IExplodable, IProjectile
{
    public float Speed { get; set; }
    public int PointsForBeingDestroyed { get; set; }
    public ExplosionStats ExplosionStats { get; set; }

    private Rigidbody2D _rigidbody2D;
    protected Action<Airplane> returnToPool;
    private Vector3 from;
    private Vector3 to;
    private Vector3 directionToDestination;
    private ExplosionPool explosionPool;
    private float previousDistance;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        explosionPool = FindObjectOfType<ExplosionPool>();
    }

    public void Launch(Vector3 from, Vector3 to)
    {
        this.from = from;
        this.to = to;
        directionToDestination = (to - from).normalized;
        transform.SetPositionAndRotation(from, Quaternion.LookRotation(Vector3.forward, directionToDestination));
        previousDistance = Mathf.Infinity;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDisable()
    {
        ReturnToPool();
    }

    private void Move()
    {
        float distance = Vector2.Distance(transform.position, to);
        if (distance > previousDistance)
        {
            Explode();
            Die();
        }
        else
        {
            previousDistance = distance;
            _rigidbody2D.MovePosition(new Vector3(transform.position.x, transform.position.y, 0f) + Speed * Time.fixedDeltaTime * directionToDestination);
        }
    }

    public void InitPoolable(Action<Airplane> action)
    {
        returnToPool = action;
    }

    public void ReturnToPool()
    {
        returnToPool?.Invoke(this);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Explode()
    {
        Explosion explosion = explosionPool.Pull();
        explosion.Setup(transform.position, ExplosionStats);
    }
}
