using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

public class Autonomous : MonoBehaviour
{
    public float Frequency { get; set; }
    public int PointsForBeingDestroyed { get; set; }
    public int Speed { get; set; }
    public ExplosionStats ExplosionStats { get; set; }

    public UnityAction<Attacker> OnBeingDestroyed;

    public ICommandSpawns Commander { get; set; }
    public bool CanFire { get => true; }
    public Vector3 Position { get => transform.position; }

    private Launcher launcher;
    private Pool<Missile> missilePool;
    private TargetManager targetManager;
    private Coroutine coroutine;

    private void Awake()
    {
        targetManager = FindObjectOfType<TargetManager>();
        missilePool = FindObjectOfType<Pool<Missile>>();
        launcher = GetComponent<Launcher>();
    }

    private void OnEnable()
    {
        coroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Frequency);

            if (targetManager.GetRandomTargetablePosition(out Vector3 pos))
            {
                Missile missile = missilePool.Pull();

                missile.Speed = Speed;
                missile.PointsForBeingDestroyed = PointsForBeingDestroyed;
                missile.ExplosionStats = ExplosionStats;

                launcher.Launch(missile, transform.position, pos);
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
}
