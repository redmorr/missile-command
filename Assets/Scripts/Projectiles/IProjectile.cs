using UnityEngine;

public interface IProjectile
{
    public void Launch(Vector2 from, Vector2 to, ProjectileData projectileData);
}
