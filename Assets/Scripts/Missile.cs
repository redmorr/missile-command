using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour, IPoolable<Missile>, IDestructible, IPointsOnDestroyed, IExplodable
{
    [SerializeField] private float DistanceThereshold; // TODO: Calculate from fixed time step * speed.

    public int PointsForBeingDestroyed { get; private set; }
    public ExplosionStats ExplosionStats { get; set; }

    private Rigidbody2D _rigidbody2D;
    private Action<Missile> returnToPool;
    private Vector2 startPoint;
    private Vector2 destinationPoint;
    private Vector2 directionToDestination;
    private float speed;
    private ExplosionPool explosionPool;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        explosionPool = FindObjectOfType<ExplosionPool>();
    }

    public void Setup(Vector3 position, Quaternion rotation, Vector2 startPoint, Vector2 destinationPoint, float speed, int points)
    {
        transform.SetPositionAndRotation(position, rotation);
        this.startPoint = startPoint;
        this.destinationPoint = destinationPoint;
        this.speed = speed;
        PointsForBeingDestroyed = points;

        directionToDestination = (destinationPoint - startPoint).normalized;
    }

    public void SetupExplosion(ExplosionStats explosionStats)
    {
        ExplosionStats = explosionStats;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDisable()
    {
        ReturnToPool();
    }

    public void InitPoolable(Action<Missile> action)
    {
        returnToPool = action;
    }

    public void ReturnToPool()
    {
        returnToPool?.Invoke(this);
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, destinationPoint) < DistanceThereshold)
        {
            Explode();
            Die();
        }

        _rigidbody2D.MovePosition(new Vector2(transform.position.x, transform.position.y) + speed * Time.fixedDeltaTime * directionToDestination);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Explode()
    {
        Explosion explosion = explosionPool.Pull;
        explosion.Setup(transform.position, ExplosionStats);
    }
}
