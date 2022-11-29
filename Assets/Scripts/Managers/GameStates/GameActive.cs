
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameActive : IState
{
    private readonly SpawnList spawnList;
    private readonly TargetManager targetManager;

    public GameActive(SpawnList spawnList, TargetManager targetManager)
    {
        this.spawnList = spawnList;
        this.targetManager = targetManager;
    }

    public void OnEnter()
    {
        Debug.Log("GameActive enter");
        targetManager.ReactivateAll();
        spawnList.BeginNextRound();
    }

    public void OnExit()
    {
        Debug.Log("GameActive exit");
    }

    public void Tick()
    {

    }
}
