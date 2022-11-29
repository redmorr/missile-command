public class Transition : IState
{
    private RoundTimer roundTimer;

    public Transition(RoundTimer roundTimer)
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
