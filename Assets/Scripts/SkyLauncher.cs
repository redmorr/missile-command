using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyLauncher : MonoBehaviour, ILaunchMissile
{

    [SerializeField] private Launcher Launcher;

    public bool CanFire => true;

    public Vector3 Position => transform.position;

    public void Launch(Vector3 target)
    {
        Launcher.Launch(transform.position, target);
    }

}
