using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnded : IState
{
    private SpawnList spawnList;

    public GameEnded(SpawnList spawnList)
    {
        this.spawnList = spawnList;
    }

    public void OnEnter()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void OnExit()
    {
        Debug.Log("GameEnded exit");
        
    }

    public void Tick()
    {

    }
}
