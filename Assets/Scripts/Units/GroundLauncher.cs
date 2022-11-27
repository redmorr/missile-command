using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GroundLauncher : MonoBehaviour, ILaunchMissile
{
    [SerializeField] private Launcher launcher;
    [SerializeField] private Targeter targeter;
    [SerializeField] private AmmoCounter ammo;

    public bool CanFire => isActiveAndEnabled && ammo.HasAmmo;

    public Vector3 Position => transform.position;

    public void Launch(Vector3 target)
    {
        Vector3 calculatedTarget = targeter.CalculateTarget(target);
        launcher.Launch(transform.position, calculatedTarget);
    }
}
