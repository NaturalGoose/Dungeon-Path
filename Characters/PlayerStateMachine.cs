using Godot;
using System;

public class PlayerStateMachine : FiniteStateMachine
{
    Character Player;
    AnimatedSprite AnimatedPlayerSprite;
    bool isHurt = false;
    public override void _Ready()
    {
        Player = this.GetParent<Character>();
        AnimatedPlayerSprite = (AnimatedSprite) Player.CharAnimatedSprite;
        AddState("IdleState");
        AddState("WalkState");
        AddState("HurtState");
        AddState("DeadState");
        SetState("IdleState");
    }
    public override void StateLogic(float delta)
    {
        if (CurrentState == "IdleState" || CurrentState == "WalkState")
        {
            Player Player = GetParent<Player>();
            Player.GetInput();
            Player.Move();
        }
    }
    public override string Transition(float delta)
    {
        if (CurrentState == "IdleState")
        {
            if (Player.Velocity.Length() > 10){return "WalkState";}
        }
        if (CurrentState == "WalkState")
        {
            if (Player.Velocity.Length() < 10){return "IdleState";}
        }
        if (CurrentState == "HurtState")
        {
           if (!Player.GetNode<AnimationPlayer>("AnimationPlayer").IsPlaying()){return "IdleState";}
        }
        return null;
    }
    public override void EnterState(string NewState, string OldState)
    {
        Node Player = GetParent<Node>();
        if (NewState == "IdleState"){Player.GetNode<AnimationPlayer>("AnimationPlayer").Play("Idle");}
        if (NewState == "WalkState"){Player.GetNode<AnimationPlayer>("AnimationPlayer").Play("Walk");}
        if (NewState == "HurtState"){Player.GetNode<AnimationPlayer>("AnimationPlayer").Play("Hurt");Player.GetNode<AnimationPlayer>("IFrameAnimation").Play("IFrames");}
        if (NewState == "DeadState"){Player.GetNode<AnimationPlayer>("AnimationPlayer").Play("Dead");}
    }
}