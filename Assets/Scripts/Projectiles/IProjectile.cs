using UnityEngine;

public interface IProjectile
{
    public void Setup(float speed, int pointsForBeingDestroyed, ExplosionStats explosionStats);
    public void Launch(Vector3 from, Vector3 to);
}
