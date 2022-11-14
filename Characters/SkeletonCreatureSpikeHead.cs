using Godot;
using System;

public class SkeletonCreatureSpikeHead : Enemy
{
    public override void _Process(float delta)
    {
        Hitbox Hitbox = this.GetNode<Hitbox>("Hitbox");
        Hitbox.KnockbackDirection = (Velocity);
    }
}
