using UnityEngine;
using UnityEngine.Events;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int InitialAmmoSize;
    [SerializeField] private bool InfiniteAmmo;

    public UnityAction<int> OnAmmoChanged;

    public int CurrentAmmo { get; private set; }
    public int InitialAmmo { get => InitialAmmoSize; }


    private void Awake()
    {
        CurrentAmmo = InitialAmmoSize;
    }

    public bool GetMissile(out Missile missile)
    {
        missile = null;
        if (CurrentAmmo > 0 || InfiniteAmmo)
        {
            missile = MissilePool.Instance.Pull;
            CurrentAmmo--;
            OnAmmoChanged?.Invoke(CurrentAmmo);
            return true;
        }
        return false;
    }
}
