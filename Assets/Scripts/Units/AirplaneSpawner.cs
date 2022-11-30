using UnityEngine;

public class AirplaneSpawner : MonoBehaviour, ISpawner
{
    private readonly float screenLength = 80f;

    [SerializeField] private ObjectPool<Airplane> airplanePool;
    [SerializeField] private ProjectileData airplaneMissileData;
    [SerializeField] private ProjectileData airplaneMissileLauncherData;
    [SerializeField] private float attackFrequency;

    private IOrderUnitSpawn attackCommander;
    public Vector3 Position { get => transform.position; }

    private void Awake()
    {
        attackCommander = GetComponentInParent<IOrderUnitSpawn>();
    }

    public IAutoAttacker Spawn()
    {
        Airplane airplane = airplanePool.Pull();
        Missile projectile = airplane.GetComponent<Missile>();
        airplane.Activate(attackFrequency, airplaneMissileLauncherData);
        projectile.Launch(transform.position, transform.position + transform.right * screenLength, airplaneMissileData);
        return airplane;
    }

    private void OnEnable()
    {
        attackCommander.Register(this);
    }

    private void OnDisable()
    {
        attackCommander.Deregister(this);
    }
}
