using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCommander : MonoBehaviour, ICommander
{
    public List<ILaunchMissile> launchers;

    private void Awake()
    {
        launchers = GetComponentsInChildren<ILaunchMissile>().ToList();

        foreach (var item in launchers)
        {
            item.EnemyCommander = this;
            item.OnBeingDestroyed += Unregister;
        }
    }

    public void OrderAttack(Vector3 target)
    {
        if (GetMissileShooter(out ILaunchMissile launcher))
            launcher.Launch(target);
    }

    public void Register(ILaunchMissile newSkyLauncher)
    {
        launchers.Add(newSkyLauncher);
        newSkyLauncher.EnemyCommander = this;
        newSkyLauncher.OnBeingDestroyed += Unregister;
    }

    private void Unregister(ILaunchMissile launcher)
    {
        launchers.Remove(launcher);
        launcher.OnBeingDestroyed -= Unregister;
    }

    private bool GetMissileShooter(out ILaunchMissile missileShooter)
    {
        missileShooter = null;
        var randomIndex = Random.Range(0, launchers.Count);

        if (launchers[randomIndex] )
        {
            missileShooter = launchers[randomIndex];
            return true;
        }

        return false;
    }


}
