using System;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager>
{
    private StateMachine stateMachine;
    private TargetManager targetManager;
    private SpawnList spawnList;
    private RoundTimer roundTimer;
    private ObjectPool<Projectile> missilePool;


    protected override void Awake()
    {
        base.Awake();
        targetManager = FindObjectOfType<TargetManager>();
        missilePool = FindObjectOfType<ObjectPool<Projectile>>();
        roundTimer = GetComponent<RoundTimer>();
        spawnList = GetComponent<SpawnList>();

        stateMachine = new StateMachine();

        var roundTransition = new RoundTransition(roundTimer);
        var gameActive = new GameActive(spawnList, targetManager);
        var gameEnded = new GameEnded(spawnList);

        stateMachine.AddTransition(roundTransition, gameActive, TimePassed());
        stateMachine.AddTransition(gameActive, roundTransition, RoundFinished());
        stateMachine.AddTransition(gameActive, gameEnded, PlayerStructuresDestroyed());

        Func<bool> TimePassed() => () => roundTimer.TimerEnded;
        Func<bool> RoundFinished() => () => !spawnList.IsRoundOngoing && missilePool.CurrentlyActive == 0;
        Func<bool> PlayerStructuresDestroyed() => () => targetManager.PlayerStructuresNumber <= 0;

        stateMachine.SetState(roundTransition);
    }

    private void Update()
    {
        stateMachine.Tick();
    }
}
