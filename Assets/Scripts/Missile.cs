using System;
using UnityEngine;

public class Missile : MonoBehaviour, IPoolable<Missile>, IDestructible, IPointsOnDestroyed, IExplodable
{
    [SerializeField] private float DistanceThereshold; // TODO: Calculate from fixed time step * speed.

    public int PointsForBeingDestroyed { get; private set; }
    public ExplosionStats ExplosionStats { get; private set; }

    private Rigidbody2D _rigidbody2D;
    private Action<Missile> returnToPool;
    private Vector2 from;
    private Vector2 to;
    private float speed;
    private float time;
    private float timetoReachDestination;
    private ExplosionPool explosionPool;
    private Vector3 directionToTarget;
    private Vector3 posAfter1second;

    private TrailRenderer trailRenderer;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        explosionPool = FindObjectOfType<ExplosionPool>();
    }

    private void OnEnable()
    {
        trailRenderer.Clear();
    }

    public void Setup(Vector3 from, Vector3 to, float speed, int points, ExplosionStats explosionStats)
    {
        directionToTarget = to - from;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
        this.from = from;
        this.to = to;
        this.speed = speed;
        PointsForBeingDestroyed = points;
        ExplosionStats = explosionStats;

        time = 0f;
        timetoReachDestination = directionToTarget.magnitude / speed;
        posAfter1second = from + directionToTarget.normalized * speed;
        trailRenderer.enabled = true;
    }

    private void Update()
    {
        Move();
    }

    private void OnDisable()
    {
        trailRenderer.enabled = false;
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
        time += Time.deltaTime;
        transform.position = Vector3.LerpUnclamped(from, posAfter1second, time);
        if (time > timetoReachDestination)
        {
            Explode();
            Die();
        }
    }

    public void Die()
    {
        time = 0;
        gameObject.SetActive(false);
    }

    public void Explode()
    {
        Explosion explosion = explosionPool.Pull;
        Debug.Log(gameObject.name);
        explosion.Owner = gameObject;
        explosion.Setup(transform.position, ExplosionStats);
    }
}
