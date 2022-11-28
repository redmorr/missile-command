using UnityEngine;
using UnityEngine.Events;

public class Launcher : MonoBehaviour
{
    public UnityAction<Vector3> OnLaunch;

    private AmmoCounter ammoCounter;

    private void Awake()
    {
        TryGetComponent(out ammoCounter);
    }

    public void Launch(IProjectile projectile, Vector3 from, Vector3 to)
    {
        if (!ammoCounter || ammoCounter.HasAmmo)
        {
            projectile.Launch(from, to);
            OnLaunch?.Invoke(to);
        }
    }
}
