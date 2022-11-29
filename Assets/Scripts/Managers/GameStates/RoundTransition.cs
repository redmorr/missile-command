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
    }

    public void OnExit()
    {
        roundTimer.ResetTimer();
    }

    public void Tick()
    {

    }
}
