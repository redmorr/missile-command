using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Launcher : MonoBehaviour
{
    [SerializeField] private float MissileSpeed = 5f;

    public bool CanFire { get => ammo.CurrentAmmo > 0; }

    public UnityAction<Vector3> OnLaunch;

    private Ammo ammo;

    private void Awake()
    {
        ammo = GetComponent<Ammo>();
    }

    public void Launch(Vector3 from, Vector3 to)
    {
        if (ammo.GetMissile(out Missile missile))
        {
            Vector3 directionToTarget = to - from;
            missile.Setup(from, Quaternion.LookRotation(Vector3.forward, directionToTarget), from, to, MissileSpeed);
            OnLaunch?.Invoke(to);
        }
    }
}
