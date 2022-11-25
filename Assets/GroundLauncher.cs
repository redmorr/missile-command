using UnityEngine;

public class GroundLauncher : MonoBehaviour, ILaunchMissile
{
    [SerializeField] private Launcher launcher;
    [SerializeField] private Targeter targeter;
    [SerializeField] private Ammo ammo;

    public bool CanFire => ammo.CurrentAmmo > 0;

    public Vector3 Position => transform.position;

    public void Launch(Vector3 target)
    {
        Vector3 calculatedTarget = targeter.CalculateTarget(target);
        launcher.Launch(transform.position, calculatedTarget);
    }
}
