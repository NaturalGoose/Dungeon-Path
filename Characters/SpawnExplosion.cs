using Godot;
using System;
public class SpawnExplosion : AnimatedSprite
{
    public override void _Ready()
    {
        this.Play("SpawnExplode");
    }
    void SpawnExplodeAnimOver()
    {
        this.QueueFree();
    }
}
