
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameActive : IState
{
    private SpawnList spawnList;

    public GameActive(SpawnList spawnList)
    {
        this.spawnList = spawnList;
    }

    public void OnEnter()
    {
        Debug.Log("GameActive enter");
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
