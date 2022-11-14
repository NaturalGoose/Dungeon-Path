using Godot;
using System;

public class SkeletonSpikeHeadStateMachine : FiniteStateMachine
{
    Enemy Enemy;
    AnimationPlayer EnemyAnimationPlayer;
      public override void _Ready()
    {
        Enemy = GetParent<Enemy>();
        EnemyAnimationPlayer = Enemy.GetNode<AnimationPlayer>("AnimationPlayer");

        AddState("ChasingState");
        AddState("HurtState");
        AddState("DeadState");
        SetState("ChasingState");
    }
    public override void StateLogic(float delta)
    {
        Enemy Enemy = GetParent<Enemy>();
        if (CurrentState == "ChasingState"){Enemy.Chasing(); Enemy.Move();}
    }
    public override string Transition(float delta)
    {
        if (CurrentState == "HurtState")
        {
            if (!EnemyAnimationPlayer.IsPlaying()){return "ChasingState";}
        }
        return null;
    }
    public override void EnterState(string NewState, string OldState)
    {
        if (NewState == "ChasingState"){EnemyAnimationPlayer.Play("Walk");}
        if (NewState == "HurtState"){EnemyAnimationPlayer.Play("Hurt");}
        if (NewState == "DeadState"){EnemyAnimationPlayer.Play("Dead");}
    }
}
