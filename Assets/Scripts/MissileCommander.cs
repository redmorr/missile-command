using UnityEngine;

public class MissileCommander : MonoBehaviour, ICommander
{
    private ILaunchMissile[] launchers;

    private void Awake()
    {
        launchers = GetComponentsInChildren<ILaunchMissile>();
    }

    public void OrderAttack(Vector3 target)
    {
        if (GetClosestMissileShooter(out ILaunchMissile launcher, target))
            launcher.Launch(target);
    }

    private bool GetClosestMissileShooter(out ILaunchMissile missileShooter, Vector3 target)
    {
        missileShooter = null;
        float minDistnace = Mathf.Infinity;

        foreach (ILaunchMissile launcher in launchers)
        {
            if (launcher.CanFire)
            {
                float distance = Vector2.Distance(target, launcher.Position);
                if (distance < minDistnace)
                {
                    minDistnace = distance;
                    missileShooter = launcher;
                }
            }
        }

        return missileShooter != null;
    }
}
