using UnityEngine.SceneManagement;

public class Ended : IState
{
    public Ended()
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
