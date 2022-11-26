using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour, IPoolable<Missile>, IDestructible, IPointsOnDestroyed, IExplodable
{
    [SerializeField] private float DistanceThereshold; // TODO: Calculate from fixed time step * speed.

    public int PointsForBeingDestroyed { get; private set; }
    public ExplosionStats ExplosionStats { get; private set; }

    private Rigidbody2D _rigidbody2D;
    private Action<Missile> returnToPool;
    private Vector2 startPoint;
    private Vector2 destinationPoint;
    private Vector2 directionToDestination;
    private float speed;
    private ExplosionPool explosionPool;
    private float previousDistance;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        explosionPool = FindObjectOfType<ExplosionPool>();
    }

    public void Setup(Vector3 position, Quaternion rotation, Vector2 startPoint, Vector2 destinationPoint, float speed, int points, ExplosionStats explosionStats)
    {
        transform.SetPositionAndRotation(position, rotation);
        this.startPoint = startPoint;
        this.destinationPoint = destinationPoint;
        this.speed = speed;
        PointsForBeingDestroyed = points;
        ExplosionStats = explosionStats;
        previousDistance = Mathf.Infinity;
        directionToDestination = (destinationPoint - startPoint).normalized;
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
        float distance = Vector2.Distance(transform.position, destinationPoint);
        if (distance > previousDistance)
        {
            Explode();
            Die();
        }
        else
        {
            previousDistance = distance;
            _rigidbody2D.MovePosition(new Vector2(transform.position.x, transform.position.y) + speed * Time.fixedDeltaTime * directionToDestination);
        }
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
