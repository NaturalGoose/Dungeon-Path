using Godot;
using System;
using System.Collections.Generic;

public class MainRoom : Node2D
{
	PackedScene SpawnExplosionScene = GD.Load<PackedScene>("res://Characters/SpawnExplosion.tscn");
	PackedScene Skeleton_Spike_Head = GD.Load<PackedScene>("res://Characters/SkeletonCreatureSpikeHead.tscn");
	TileMap SecondTileMap;
	Node2D Door_Container;
	Node2D Enemy_Spawns;
	Node2D Entrance;
	Area2D Player_Detector;
	[Export] int Rounds = 2;
	[Export] int DefaultNumEnemies = 2;
	int CurrentRound;
	int NumEnemies;

	public override void _Ready()
	{
		SecondTileMap = GetNode<TileMap>("TileMap2");
		Door_Container = GetNode<Node2D>("Doors");
		Enemy_Spawns = GetNode<Node2D>("EnemySpawns");
		Entrance = GetNode<Node2D>("EntranceNode");
		Player_Detector = GetNode<Area2D>("PlayerDetector");
		DefaultNumEnemies = Enemy_Spawns.GetChildCount();
		CurrentRound = Rounds;
		NumEnemies = DefaultNumEnemies;
	}
	public void OpenEntrances()
	{
		foreach(Door door in Door_Container.GetChildren())
		{
			door.OpenDoor();
		}
		foreach(StaticBody2D entrance in Entrance.GetChildren())
		{
			var EntranceCollision = entrance.GetNode<CollisionShape2D>("EntranceCollision");
			EntranceCollision.SetDeferred("disabled", false);
		}
	}
	public virtual void CloseEntrances()
	{
		foreach(StaticBody2D entrance in Entrance.GetChildren())
		{
			var EntranceCollision = entrance.GetNode<CollisionShape2D>("EntranceCollision");
			EntranceCollision.SetDeferred("disabled", false);
		}
	}
	public void SpawnEnemies()
	{
		foreach(Position2D EnemyPosition in Enemy_Spawns.GetChildren())
		{
			var Enemy = Skeleton_Spike_Head.Instance<Enemy>();
			EnemyPosition.CallDeferred("add_child", Enemy);
			Enemy.Connect("tree_exited", this, "OnEnemyKilled");
			Enemy.Position = Vector2.Zero;
			Enemy.Visible = true;
			AnimatedSprite SpawnExplosion = SpawnExplosionScene.Instance<AnimatedSprite>();
			EnemyPosition.CallDeferred("add_child", SpawnExplosion);
			SpawnExplosion.Position = Vector2.Zero;
		}   
	}
	public void OnEnemyKilled()
	{
		NumEnemies -= 1;
		if (NumEnemies == 0 && CurrentRound <= 0)
		{
			OpenEntrances();
			foreach(StaticBody2D entrance in Entrance.GetChildren())
			{
				CollisionShape2D EntranceCollision = entrance.GetNode<CollisionShape2D>("EntranceCollision");
				EntranceCollision.SetDeferred("disabled", true);
			}
		}
		else if(NumEnemies == 0 && CurrentRound > 0)
		{
			NumEnemies = DefaultNumEnemies;
			CurrentRound -= 1;
			SpawnEnemies();
		}
		
	}
	public void PlayerEnteredEntrance(Node Other)
	{
		if(NumEnemies > 0)
		{
			CloseEntrances();
			SpawnEnemies();
		}
		else
		{
			OpenEntrances();
		}
		Player_Detector.QueueFree();
	}
}
