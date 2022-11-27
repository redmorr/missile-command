using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkyLauncher : ILaunchMissile
{
    [SerializeField] private Launcher Launcher;

    private IProjectileProvider projectileProvider;

    private void Awake()
    {
        projectileProvider = GetComponent<IProjectileProvider>();
    }

    public override void Launch(Vector3 target)
    {
        IProjectile projectile = projectileProvider.GetProjectile();
        Launcher.Launch(projectile, transform.position, target);
    }

}
