using UnityEngine;

public class MissileShooter : MonoBehaviour
{
    [SerializeField] private Transform WeaponPivot;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private Missile MissilePrefab;
    [SerializeField] private float MissileSpeed = 5f;

    private static Pool<Missile> missilePool;

    private MissileCommander missileCommander;

    public float Ammo { get; private set; }

    private void Awake()
    {
        missilePool = new Pool<Missile>(MissilePrefab, 5);
        Ammo = 1;
    }

    public void Setup(MissileCommander missileCommander)
    {
        this.missileCommander = missileCommander;
    }

    private void Update()
    {
        Vector2 directionToMouse = missileCommander.MousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, directionToMouse);
        WeaponPivot.eulerAngles = new Vector3(0, 0, angle);
    }

    public void FireMissile()
    {
        Missile missile = missilePool.Get();
        missile.Setup(SpawnPoint.position, SpawnPoint.rotation, SpawnPoint.position, missileCommander.MousePosition, MissileSpeed);
    }
}
