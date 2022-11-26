using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Explosion : MonoBehaviour, IPoolable<Explosion>
{
    public GameObject Owner;
    private Action<Explosion> returnToPool;
    private float duration;
    private Vector3 minScale;
    private Vector3 maxScale;
    private float time;

    public void Setup(Vector2 position, ExplosionStats explosionStats)
    {
        duration = explosionStats.Duration;
        minScale = new Vector3(explosionStats.StartRadius, explosionStats.StartRadius, explosionStats.StartRadius);
        maxScale = new Vector3(explosionStats.EndRadius, explosionStats.EndRadius, explosionStats.EndRadius);
        transform.position = position;
        time = 0f;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(minScale, maxScale, time / duration);
        time += Time.deltaTime;

        if (time > duration)
        {
            gameObject.SetActive(false);
            ReturnToPool();
        }
    }

    public void InitPoolable(Action<Explosion> action)
    {
        returnToPool = action;
    }

    public void ReturnToPool()
    {
        returnToPool?.Invoke(this);
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
        ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(name);
        if (collision.TryGetComponent(out IDestructible destructible))
        {
            if (destructible is IPointsOnDestroyed)
                ScoreKeeper.Instance.AddScore((destructible as IPointsOnDestroyed).PointsForBeingDestroyed);

            destructible.Die();

            if (destructible is IExplodable)
                (destructible as IExplodable).Explode();

        }
    }
}
