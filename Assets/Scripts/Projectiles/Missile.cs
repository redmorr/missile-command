using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour, IPoolable<Missile>, IDestructible, IPointsOnDestroyed, IExplodable, IProjectile
{
    [SerializeField] private float DistanceThereshold; // TODO: Calculate from fixed time step * speed.
    [SerializeField] private int _PointsForBeingDestroyed;
    [SerializeField] private int Speed;
    [SerializeField] private ExplosionStats _ExplosionStats;

    public int PointsForBeingDestroyed { get => _PointsForBeingDestroyed; set => _PointsForBeingDestroyed = value; }
    public ExplosionStats ExplosionStats { get => _ExplosionStats; set => _ExplosionStats = value; }

    private Rigidbody2D _rigidbody2D;
    private Action<Missile> returnToPool;
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

    public void Setup(Vector3 from, Vector3 to)
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

    public void InitPoolable(Action<Missile> action)
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
        Explosion explosion = explosionPool.Get();
        explosion.Setup(transform.position, ExplosionStats);
    }
}
