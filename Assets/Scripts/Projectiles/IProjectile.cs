using UnityEngine;

public interface IProjectile
{
    public void Setup(float speed, int pointsForBeingDestroyed, ExplosionStats explosionStats);
    public void Launch(Vector2 from, Vector2 to);
}
