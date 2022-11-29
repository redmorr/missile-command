using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerStructure : MonoBehaviour, IDestructible, IExplodable
{
    [SerializeField] private ExplosionStats explosionStats;

    private Rigidbody2D _rigidbody2D;
    private ExplosionPool explosionPool;

    public UnityAction<PlayerStructure> OnBeingDestroyed;

    public Vector3 Position { get => transform.position; }

    private void Awake()
    {
        explosionPool = FindObjectOfType<ExplosionPool>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Die()
    {
        OnBeingDestroyed?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void Explode()
    {
        Explosion explosion = explosionPool.Pull();
        explosion.Setup(transform.position, explosionStats);
    }
}
