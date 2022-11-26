using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Targetable : MonoBehaviour, IDestructible, IPointsOnSurvived
{
    [SerializeReference] private int Points;

    public UnityAction<Targetable> OnBeingDestroyed;

    private Rigidbody2D _rigidbody2D;

    public Vector3 Position { get => transform.position; }

    public int PointsForSurviving { get => Points; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Die()
    {
        OnBeingDestroyed?.Invoke(this);
        gameObject.SetActive(false);
    }
}
