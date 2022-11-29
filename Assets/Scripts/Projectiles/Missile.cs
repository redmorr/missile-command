using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour, IPoolable<Missile>, IDestructible, IPointsOnDestroyed, IExplodable, IProjectile
{
    private Rigidbody2D _rigidbody2D;
    private Action<Missile> returnToPool;
    private ExplosionPool explosionPool;
    private ProjectileData missileData;
    private Vector3 to;
    private Vector3 directionToDestination;

    public int PointsForBeingDestroyed { get => missileData.PointsForBeingDestroyed; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        explosionPool = FindObjectOfType<ExplosionPool>();
    }

    public void Launch(Vector2 from, Vector2 to, ProjectileData projectileData)
    {
        missileData = projectileData;
        this.to = to;
        directionToDestination = (to - from).normalized;
        transform.SetPositionAndRotation(from, Quaternion.LookRotation(Vector3.forward, directionToDestination));
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float distance = Vector2.Distance(transform.position, to);
        if (distance < 0.5f)
        {
            Explode();
            Die();
        }
        else
        {
            _rigidbody2D.MovePosition(new Vector3(transform.position.x, transform.position.y, 0f) + missileData.Speed * Time.fixedDeltaTime * directionToDestination);
        }
    }

    public void Explode()
    {
        Explosion explosion = explosionPool.Pull();
        explosion.Setup(transform.position, missileData.explosionStats);
    }

    public void InitPoolable(Action<Missile> action) => returnToPool = action;

    public void ReturnToPool() => returnToPool?.Invoke(this);

    public void Die() => gameObject.SetActive(false);

    private void OnDisable() => ReturnToPool();
}
