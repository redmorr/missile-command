using UnityEngine;
using UnityEngine.InputSystem;

public class MissileShooter : MonoBehaviour
{
    [SerializeField] private Transform WeaponPivot;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private Missile MissilePrefab;
    [SerializeField] private float MissileSpeed = 5f;

    public float Ammo { get; private set; }

    private readonly float maxShootAngle = 90f;
    private static Pool<Missile> missilePool;
    private MissileCommander missileCommander;
    private float shootAngle;
    private Vector2 targetPosition;

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
        shootAngle = Vector2.SignedAngle(Vector2.up, directionToMouse);
        WeaponPivot.eulerAngles = new Vector3(0, 0, Mathf.Clamp(shootAngle, -maxShootAngle, maxShootAngle));
    }

    public void FireMissile(Vector3 target)
    {
        Missile missile = missilePool.Get();
        missile.Setup(SpawnPoint.position, SpawnPoint.rotation, SpawnPoint.position, new Vector2(target.x, Mathf.Max(target.y, SpawnPoint.position.y) ), MissileSpeed);
    }

}
