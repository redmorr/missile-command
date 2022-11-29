using UnityEngine;

public class GameEnded : IState
{
    private SpawnList spawnList;

    public GameEnded(SpawnList spawnList)
    {
        this.spawnList = spawnList;
    }

    public void OnEnter()
    {
        Debug.Log("GameEnded enter");
    }

    public void OnExit()
    {
        Debug.Log("GameEnded exit");
        spawnList.ResetToRoundOne();
    }

    public void Tick()
    {

    }
}
