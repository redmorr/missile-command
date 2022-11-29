using System;

public class GameManager : Singleton<GameManager>
{
    private StateMachine stateMachine;
    private TargetManager targetManager;
    private SpawnList spawnList;
    private RoundTimer roundTimer;
    private ObjectPool<Missile> projectilePool;

    protected override void Awake()
    {
        base.Awake();

        targetManager = FindObjectOfType<TargetManager>();
        projectilePool = FindObjectOfType<ObjectPool<Missile>>();
        roundTimer = GetComponent<RoundTimer>();
        spawnList = GetComponent<SpawnList>();

        stateMachine = new StateMachine();

        var transition = new Transition(roundTimer);
        var active = new Active(spawnList, targetManager);
        var ended = new Ended();

        stateMachine.AddTransition(transition, active, TimePassed());
        stateMachine.AddTransition(active, transition, RoundFinishedAndNoActiveProjectiles());
        stateMachine.AddTransition(active, ended, PlayerDead());

        Func<bool> TimePassed() => () => roundTimer.TimerStopped;
        Func<bool> RoundFinishedAndNoActiveProjectiles() => () => !spawnList.IsRoundOngoing && projectilePool.CurrentlyActive == 0;
        Func<bool> PlayerDead() => () => targetManager.PlayerStructuresNumber <= 0;

        stateMachine.SetState(transition);
    }

    private void Update()
    {
        stateMachine.Tick();
    }
}
