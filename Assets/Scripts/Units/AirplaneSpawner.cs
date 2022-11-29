using UnityEngine;

public class AirplaneSpawner : MonoBehaviour, ISpawner
{
    private readonly float screenLength = 80f;

    [SerializeField] private ObjectPool<Airplane> airplanePool;
    [SerializeField] private float frequency;
    [SerializeField] private int pointsForBeingDestroyed;
    [SerializeField] private int speed;
    [SerializeField] private ExplosionStats explosionStats;

    private IOrderUnitSpawn attackCommander;
    public Vector3 Position { get => transform.position; }

    private void Awake()
    {
        attackCommander = GetComponentInParent<IOrderUnitSpawn>();
    }

    public IAutoAttacker Spawn()
    {
        Airplane airplane = airplanePool.Pull();
        Projectile projectile = airplane.GetComponent<Projectile>();
        projectile.Setup(speed, pointsForBeingDestroyed, explosionStats);
        airplane.Setup(frequency, speed, pointsForBeingDestroyed, explosionStats);
        projectile.Launch(transform.position, transform.position + transform.right * screenLength);
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
