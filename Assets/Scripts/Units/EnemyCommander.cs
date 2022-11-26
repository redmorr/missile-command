using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCommander : MonoBehaviour, ICommander
{
    private ILaunchMissile[] launchers;

    private void Awake()
    {
        launchers = GetComponentsInChildren<ILaunchMissile>();
    }

    public void OrderAttack(Vector3 target)
    {
        if (GetMissileShooter(out ILaunchMissile launcher))
            launcher.Launch(target);
    }

    private bool GetMissileShooter(out ILaunchMissile missileShooter)
    {
        missileShooter = null;
        var randomIndex = Random.Range(0, launchers.Length);

        if (launchers[randomIndex].CanFire)
        {
            missileShooter = launchers[randomIndex];
            return true;
        }

        return false;
    }

}
