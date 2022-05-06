using System;
using Godot;
using System.Collections.Generic;

public class Level : Node2D
{
    public String LevelName { get => "Level" + _levelIndex; }

    private int _levelIndex;
    public int LevelIndex { get => _levelIndex; set => _levelIndex = value; }

    private Dictionary<int, Door> _doorIndexMap = new Dictionary<int, Door>();
    public Dictionary<int, Door> DoorIndexMap { get => _doorIndexMap; }

    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is Door)
            {
                var door = child as Door;
                _doorIndexMap.Add(door.DoorIndex, door);
            }
        }
    }

    // public void ChangeLevel(String NextLevelName)
    // {
    //     GetNode<GameEvents>("/root/GameEvents")
    //         .EmitSignal("LevelChanged", NextLevelName);
    // }
}
