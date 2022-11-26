using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour, IPoolable<Missile>, IDestructible, IPointsOnDestroyed
{
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private float DistanceThereshold; // TODO: Calculate from fixed time step * speed.
    [SerializeField] private Explosion ExplosionPrefab;

    private Action<Missile> returnToPool;
    private Vector2 startPoint;
    private Vector2 destinationPoint;
    private Vector2 directionToDestination;
    private float speed;

    public int PointsForBeingDestroyed { get; private set; }

    public void Setup(Vector3 position, Quaternion rotation, Vector2 startPoint, Vector2 destinationPoint, float speed, int points)
    {
        transform.SetPositionAndRotation(position, rotation);
        this.startPoint = startPoint;
        this.destinationPoint = destinationPoint;
        this.speed = speed;
        PointsForBeingDestroyed = points;

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

    public void Init(Action<Missile> action)
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
            Die();

        Rigidbody2D.MovePosition(new Vector2(transform.position.x, transform.position.y) + speed * Time.fixedDeltaTime * directionToDestination);
    }

    public void Die()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
