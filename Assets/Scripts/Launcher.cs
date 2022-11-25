using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Launcher : MonoBehaviour
{
    [SerializeField] private Transform Muzzle;
    [SerializeField] private float MissileSpeed = 5f;

    public UnityAction<Vector3> OnLaunch;

    public bool CanFire { get => ammo.CurrentAmmo > 0; }

    private Ammo ammo;

    private void Awake()
    {
        ammo = GetComponent<Ammo>();
    }

    public void Launch(Vector3 target)
    {
        if (ammo.GetMissile(out Missile missile))
        {
            target.y = Mathf.Max(target.y, Muzzle.position.y);
            Vector3 directionToTarget = target - Muzzle.position;
            missile.Setup(Muzzle.position, Quaternion.LookRotation(Vector3.forward, directionToTarget), Muzzle.position, target, MissileSpeed);
            OnLaunch?.Invoke(target);
        }
    }
}
