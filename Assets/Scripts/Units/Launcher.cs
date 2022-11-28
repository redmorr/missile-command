using UnityEngine;
using UnityEngine.Events;

public class Launcher : MonoBehaviour
{
    public UnityAction<Vector3> OnLaunch;

    public void Launch(IProjectile projectile, Vector3 from, Vector3 to)
    {
        projectile.Launch(from, to);
        Vector3 directionToTarget = to - from;
        projectile.Launch(from, to);
        OnLaunch?.Invoke(to);
    }
}
