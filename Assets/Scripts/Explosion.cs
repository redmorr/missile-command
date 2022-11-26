using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float Duration = 1f;
    [SerializeField] private float MinimumSize = 1f;
    [SerializeField] private float MaximumSize = 3f;

    private float time;
    private Vector3 startScale;
    private Vector2 finalScale;

    private void Awake()
    {
        startScale = new Vector3(MinimumSize, MinimumSize, MinimumSize);
        finalScale = new Vector3(MaximumSize, MaximumSize, MaximumSize);
    }

    private void Start()
    {
        time = 0f;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(startScale, finalScale, time / Duration);
        time += Time.deltaTime;

        if (time > Duration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDestructible destructible))
        {
            if (destructible is IPointsOnDestroyed)
            {
                ScoreKeeper.Instance.AddScore((destructible as IPointsOnDestroyed).PointsForBeingDestroyed);
            }
            destructible.Die();
        }
    }
}
