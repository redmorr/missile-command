using UnityEngine.SceneManagement;

public class GameEnded : IState
{
    public GameEnded()
    {

    }

    public void OnEnter()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {

    }
}
