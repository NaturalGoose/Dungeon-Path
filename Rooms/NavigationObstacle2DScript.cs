using Godot;
using System;

public class NavigationObstacle2DScript : NavigationObstacle2D
{
    public override void _Ready()
    {
    }
    public void AgentEntered(Character enemy)
    {
        GD.Print("m"); 
        this.SetNavigation(enemy.GetNode<NavigationAgent2D>("NavigationAgent2D"));

    }
}
