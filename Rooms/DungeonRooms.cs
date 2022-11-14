using Godot;
using System;
using System.Collections.Generic;

public class DungeonRooms : Node2D
{
    List<PackedScene> StartingLevel = new List<PackedScene>{GD.Load<PackedScene>("res://Rooms/RoomStart.tscn")};
    List<PackedScene> MiddleLevel = new List<PackedScene>{GD.Load<PackedScene>("res://Rooms/Room0.tscn"), 
    GD.Load<PackedScene>("res://Rooms/Room1.tscn"), GD.Load<PackedScene>("res://Rooms/Room2.tscn")};
    List<PackedScene> EndingLevel = new List<PackedScene>{GD.Load<PackedScene>("res://Rooms/RoomEnd.tscn")};
    List<PackedScene> Hallways = new List<PackedScene>{GD.Load<PackedScene>("res://Rooms/Hallway.tscn")};
    int StartingLevelAmount = 1;
    int MiddleLevelAmount = 3;
    int EndingLevelAmount = 1;
    int HallwayAmount = 1;
    Node2D Hallway = null;
    int TileSize = 16;
    int FloorTileIndex = 8;
    int RightWallTileIndex = 2; 
    int LeftWallTileIndex = 7;
    [Export] int NumberOfLevels = 3;
    KinematicBody2D Player;
    public override void _Ready()
    {
        GD.Randomize();
        Player = GetParent().GetNode<KinematicBody2D>("Player");
        SpawnRooms();
    }
    public void SpawnRooms()
    {
        Node2D PreviousRoom = null;
        Node2D CurrentRoom;
        PackedScene CurrentPackedScene;
        Vector2 StartingLocation = Vector2.Zero;
        for(int i = 0; i <= NumberOfLevels; i++)
        {
            if(i == 0){var Room = StartingLevel[(int)GD.RandRange(0,StartingLevelAmount)].Instance<Node2D>();
            CallDeferred("add_child", Room);PreviousRoom = Room;CurrentRoom = Room;GD.Print(CurrentRoom.Name);PositionPlayer(CurrentRoom);}
            else
            {
                if(i == NumberOfLevels){var Room = EndingLevel[(int)GD.RandRange(0,EndingLevelAmount)].Instance<Node2D>();CurrentRoom = Room;
                if(PreviousRoom != null){PreviousRoom.CallDeferred("add_child", Room);}else{GD.Print("m");CallDeferred("add_child", Room);}PositionRooms(PreviousRoom, CurrentRoom, i); PreviousRoom = Room;}
                else {CurrentPackedScene = MiddleLevel[(int)GD.RandRange(0,MiddleLevelAmount)];CurrentPackedScene.ResourceName = CurrentPackedScene.Instance<Node2D>().Name;
                if(PreviousRoom != null){while(CurrentPackedScene.ResourceName == PreviousRoom.Name){CurrentPackedScene = MiddleLevel[(int)GD.RandRange(0,MiddleLevelAmount)];}}
                var Room = CurrentPackedScene.Instance<Node2D>();CurrentRoom = Room;
                if(PreviousRoom != null){PreviousRoom.CallDeferred("add_child", Room);}else{GD.Print("m3");CallDeferred("add_child", Room);} CurrentRoom = Room;PositionRooms(PreviousRoom, CurrentRoom, i); PreviousRoom = Room;}  
            }
        }
    }
    public void PositionRooms(Node2D PreviousRoom, Node2D CurrentRoom, int RoomNumber)
    {
        if (PreviousRoom != null)
        {    
            foreach(Position2D Exit in PreviousRoom.GetNode<Node2D>("Exits").GetChildren())
            {
                Hallway = Hallways[(int)GD.RandRange(0,HallwayAmount)].Instance<Node2D>();
                if(PreviousRoom != null){PreviousRoom.CallDeferred("add_child", Hallway);}else{GD.Print("m1");PreviousRoom.CallDeferred("add_child", Hallway);}
                Hallway.Position = Exit.Position;
                Position2D HallwayExit = Hallway.GetNode<Position2D>("Exits/Position2D");
                CurrentRoom.Position = Exit.GlobalPosition + new Vector2(0, HallwayExit.GlobalPosition.y);
            }
        }
    }
    public void PositionPlayer(Node2D CurrentRoom)
    {
        //Position2D PlayerPos = CurrentRoom.GetNode<Position2D>("PlayerPosition");
        //Player.GlobalPosition = PlayerPos.GlobalPosition;
    }
}
