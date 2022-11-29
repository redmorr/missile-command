public class Active : IState
{
    private readonly SpawnList spawnList;
    private readonly TargetManager targetManager;

    public Active(SpawnList spawnList, TargetManager targetManager)
    {
        this.spawnList = spawnList;
        this.targetManager = targetManager;
    }

    public void OnEnter()
    {
        targetManager.ReactivateAll();
        spawnList.BeginNextRound();
    }

    public void OnExit()
    {
    }

    public void Tick()
    {

    }
}
