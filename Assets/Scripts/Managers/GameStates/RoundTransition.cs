using UnityEngine;

public class RoundTransition : IState
{
    private RoundTimer roundTimer;

    public RoundTransition(RoundTimer roundTimer)
    {
        this.roundTimer = roundTimer;
    }

    public void OnEnter()
    {
        roundTimer.StartTimer();
        Debug.Log("RoundTransition enter");

    }

    public void OnExit()
    {
        Debug.Log("RoundTransition exit");
        roundTimer.ResetTimer();
    }

    public void Tick()
    {

    }
}
