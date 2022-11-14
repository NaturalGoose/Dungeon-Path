using Godot;
using System;

public class Character: KinematicBody2D
{
    [Signal] public delegate void HealthChanged(int NewHp, string Name);
    [Export] public int Acceleration = 40;
    [Export] public int MaxSpeed = 100;   
    [Export] private int HealthProperty = 3;
    public int Health {get{return HealthProperty;} set{HealthProperty = value;SetHp(HealthProperty);}}
    public const float friction = 0.15F;
    public Vector2 MovementDirection = Vector2.Zero;
    public Vector2 Velocity = Vector2.Zero;
    public object CharAnimatedSprite;
    public override void _Ready()
    {
        CharAnimatedSprite = GetNode<object>("SpriteAnimation");
        var HealthGui = GetTree().CurrentScene.GetNode<CanvasLayer>("GUI");
        this.Connect("HealthChanged", HealthGui, "NewHealth");
        NewOnReady();
    }

    public override void _PhysicsProcess(float delta)
    {
        Velocity = MoveAndSlide(Velocity);
    }

    public void Move()
    {
        Velocity.LimitLength(MaxSpeed);
        Velocity += MovementDirection*Acceleration;
    }
    public void TakeDamage(int Damage, Vector2 KnockbackDirection, int Force, Character Body)
    {
        FiniteStateMachine StateMachine  = Body.GetNode<FiniteStateMachine>("FiniteStateMachine");
        if (Health > 0)
        {
            Health -= Damage;
            if(StateMachine is null){GD.Print("null");}
            StateMachine.SetState("HurtState");
            Velocity = KnockbackDirection*Force;
        }
        else if (Health <= 0)
        {
            StateMachine.SetState("DeadState");
            Velocity = KnockbackDirection*Force*1.5F;
        }
    }
    public void SetHp(int NewHp)
    {
        HealthProperty = NewHp;
        EmitSignal("HealthChanged", NewHp, this.Name);
    }
    public virtual void NewPhysicsProcess(float delta){}
    public virtual void NewOnReady(){}
}
