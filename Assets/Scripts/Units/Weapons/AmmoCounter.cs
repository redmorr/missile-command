using UnityEngine;
using UnityEngine.Events;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] private int initialAmmo;
    [SerializeField] private bool hasInfiniteAmmo;

    private int currentAmmo;

    public UnityAction<int> OnAmmoChanged;

    public int InitialAmmo { get => initialAmmo; }
    public int CurrentAmmo { get => currentAmmo; }
    public bool HasAmmo { get => currentAmmo > 0 || hasInfiniteAmmo; }

    private void Awake()
    {
        currentAmmo = initialAmmo;
    }

    public void SpentAmmo()
    {
        currentAmmo--;
        OnAmmoChanged?.Invoke(currentAmmo);
    }
}
