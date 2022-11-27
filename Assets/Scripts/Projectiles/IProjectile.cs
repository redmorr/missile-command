using UnityEngine;

public interface IProjectile
{
    public float Speed { get; set; }
    public void Launch(Vector3 from, Vector3 to);
}
