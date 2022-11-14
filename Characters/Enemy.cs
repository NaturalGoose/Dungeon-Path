using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Enemy : Character
{
	[Signal]public delegate void TreeEntered(Character enemy);
	AnimatedSprite Player;
	AnimatedSprite EnemySprite;
	NavigationAgent2D Navigate;
	FiniteStateMachine StateMachine;
	Node2D Raycasts;
	int AvoidForce = 1000;
	float MaxSteering = (float)10.5;
	
	public override void _Ready()
	{   
		Player = GetTree().CurrentScene.GetNode<AnimatedSprite>("Player/SpriteAnimation");
		EnemySprite = this.GetNode<AnimatedSprite>("SpriteAnimation");
		Navigate = this.GetNode<NavigationAgent2D>("NavigationAgent2D");
		StateMachine = this.GetNode<FiniteStateMachine>("FiniteStateMachine");
		Raycasts = this.GetNode<Node2D>("RayCasts");
	}

	public void Chasing()
	{
		if (Player != null)
		{
			if (EnemySprite != null)
			{
				Vector2 Steering = Vector2.Zero;
				Vector2 VectorToTarget = GetGlobalMousePosition() - GlobalPosition;
				Steering += SeekSteering(VectorToTarget);
				//Steering += AvoidObstacles();
				MovementDirection += Steering.LimitLength(MaxSteering);
			}
		}
	}

	public Vector2 SeekSteering(Vector2 VectorToTarget)
	{
		Vector2 DesiredVelocity = VectorToTarget.Normalized()*MaxSpeed;
		return DesiredVelocity-Velocity;
	}
	
	public Vector2 AvoidObstacles()
	{
		Raycasts.Rotation = MovementDirection.Angle();
		foreach(RayCast2D Ray in Raycasts.GetChildren())
		{
			var RaycastXLenght = Ray.CastTo.x;
			RaycastXLenght = Velocity.Length();
			if(Ray.IsColliding())
			{
				return (GlobalPosition-Ray.GetCollisionPoint())*AvoidForce;
			}
		}
		return Vector2.Zero;
	} 

}
