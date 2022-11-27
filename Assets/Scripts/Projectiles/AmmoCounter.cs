using UnityEngine;
using UnityEngine.Events;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] private int InitialAmmoSize;
    [SerializeField] private bool InfiniteAmmo;

    public UnityAction<int> OnAmmoChanged;

    public int InitialAmmo { get => InitialAmmoSize; }
    public bool HasAmmo { get => currentAmmo > 0 || InfiniteAmmo; }

    private Launcher launcher;
    private int currentAmmo;

    private void Awake()
    {
        launcher = GetComponent<Launcher>();
        launcher.OnLaunch += SpentAmmo;
        currentAmmo = InitialAmmoSize;
    }

    private void SpentAmmo(Vector3 _)
    {
        currentAmmo--;
        OnAmmoChanged?.Invoke(currentAmmo);
    }

    private void OnDisable()
    {
        launcher.OnLaunch -= SpentAmmo;
    }
}
