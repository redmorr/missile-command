using UnityEngine;

public class MissilePool : Singleton<MissilePool>
{
    [SerializeField] private int InitialAmmoSize;
    [SerializeField] private bool InfiniteAmmo;
    [SerializeField] private Missile MissilePrefab;

    private Pool<Missile> missilePool;

    public Missile Pull { get => missilePool.Get(); }

    protected override void Awake()
    {
        base.Awake();
        missilePool = new Pool<Missile>(MissilePrefab, InitialAmmoSize);
    }
}
