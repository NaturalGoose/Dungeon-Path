using Godot;
using System;

public class Hitbox : Area2D
{
    [Export] public int Damage = 1;
    [Export] public int KnockbackPower = 20;
    public Vector2 KnockbackDirection = Vector2.Zero;
    CollisionShape2D Collision;

    public override void _Ready()
    {
        Collision = this.GetNode<CollisionShape2D>("CollisionShape2D");
        this.Connect("body_entered", this, "HitboxConnect");
    }
    public override void _Process(float delta)
    {

    }
    void HitboxConnect(Node Body)
    {
        if (Body is Character Hit)
        {
            if(KnockbackDirection == null){GD.Print("DAMAGE IS NULL");}
            Hit.TakeDamage(Damage, KnockbackDirection, KnockbackPower, Hit);      
        }
    }
}

