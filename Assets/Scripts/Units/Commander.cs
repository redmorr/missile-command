using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Commander : MonoBehaviour, ICommander
{
    public List<Unit> launchers;

    private void Awake()
    {
        launchers = GetComponentsInChildren<Unit>().ToList();

        foreach (var item in launchers)
        {
            item.Commander = this;
            item.OnBeingDestroyed += Unregister;
        }
    }

    public void OrderAttackRandom(Vector3 target)
    {
        if (GetRandomMissileShooter(out Unit launcher))
            launcher.Launch(target);
    }

    public void OrderAttack(Vector3 target)
    {
        if (GetClosestMissileShooter(out Unit launcher, target))
            launcher.Launch(target);
    }

    public void Register(Unit newSkyLauncher)
    {
        launchers.Add(newSkyLauncher);
        newSkyLauncher.Commander = this;
        newSkyLauncher.OnBeingDestroyed += Unregister;
    }

    private void Unregister(Unit launcher)
    {
        launchers.Remove(launcher);
        launcher.OnBeingDestroyed -= Unregister;
    }

    private bool GetRandomMissileShooter(out Unit missileShooter)
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


    private bool GetClosestMissileShooter(out Unit missileShooter, Vector3 target)
    {
        missileShooter = null;
        float minDistnace = Mathf.Infinity;

        foreach (Unit launcher in launchers)
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
