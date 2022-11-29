using UnityEngine;

[CreateAssetMenu]
public class ProjectileData : ScriptableObject
{
    public float Speed;
    public int PointsForBeingDestroyed;
    public ExplosionStats explosionStats;
}
