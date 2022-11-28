using UnityEngine;
using UnityEngine.Events;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int PointsForBeingDestroyed;
    [SerializeField] private int Speed;
    [SerializeField] private ExplosionStats ExplosionStats;

    public UnityAction<Attacker> OnBeingDestroyed;

    public ICommandAttacks Commander { get; set; }
    public bool CanFire { get => true; }
    public Vector3 Position { get => transform.position; }

    private Launcher launcher;
    private Pool<Missile> missilePool;

    private void Awake()
    {
        launcher = GetComponent<Launcher>();
        missilePool = FindObjectOfType<Pool<Missile>>();
    }

    public void Launch(Vector3 target)
    {
        Missile missile = missilePool.Pull();
        missile.Setup(Speed, PointsForBeingDestroyed, ExplosionStats);
        launcher.Launch(missile, transform.position, target);
    }

    private void OnDisable()
    {
        OnBeingDestroyed?.Invoke(this);
    }
}
