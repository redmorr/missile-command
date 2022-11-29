
using System;
using System.Collections.Generic;

public class StateMachine
{
    public IState CurrentState;

    private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> currentTransitions = new List<Transition>();

    private static List<Transition> EmptyTransitions = new List<Transition>(0);

    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
            SetState(transition.To);

        CurrentState?.Tick();
    }

    public void SetState(IState state)
    {
        if (state == CurrentState)
            return;

        CurrentState?.OnExit();
        CurrentState = state;

        transitions.TryGetValue(CurrentState.GetType(), out currentTransitions);
        if (currentTransitions == null)
            currentTransitions = EmptyTransitions;

        CurrentState.OnEnter();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (this.transitions.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            this.transitions[from.GetType()] = transitions;
        }

        transitions.Add(new Transition(to, predicate));
    }

    private class Transition
    {
        public Func<bool> Condition { get; }
        public IState To { get; }

        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    private Transition GetTransition()
    {
        foreach (var transition in currentTransitions)
            if (transition.Condition())
                return transition;

        return null;
    }
}
