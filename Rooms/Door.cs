using Godot;
using System;

public class Door : StaticBody2D
{
    AnimationPlayer DoorAnimationPlayer;
    public override void _Ready()
    {
        DoorAnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
    }
    public void OpenDoor(){DoorAnimationPlayer.Play("Open");}
}
