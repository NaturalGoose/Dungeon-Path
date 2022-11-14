using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class FiniteStateMachine : Node2D
{
    public string CurrentState = null;
    public string PreviousState = null;
    public List<string> States = new List<string>();
    public virtual void StateLogic(float delta){}
    public virtual string Transition(float delta){return null;}
    public virtual void EnterState(string NewState, string OldState){}
    public virtual void ExitState(string OldState, string NewState){}
    public void AddState(string StateName){States.Add(StateName);}
    public virtual void SetState(string NewState)
    {
        var StateCheck = States.Where(States => States.Contains(NewState));
        PreviousState = CurrentState; 
        CurrentState = NewState;
        if (PreviousState != null){ExitState(PreviousState, NewState);}
        if (StateCheck != null){EnterState(NewState, PreviousState);}
    }
    public override void _PhysicsProcess(float delta)
    {
        if (CurrentState != null)
        {
            StateLogic(delta);
            string TransitionFunc = Transition(delta);
            if (TransitionFunc != null){SetState(TransitionFunc);}
        }
    }
}
