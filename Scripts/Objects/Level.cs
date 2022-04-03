using System;
using Godot;

public class Level : Node2D
{
    [Export]
    private String _levelName;

    public String LevelName { get => _levelName; }

    public override void _Ready()
    {
        GameEvents gameEvents = GetNode<GameEvents>("/root/GameEvents");
    }

    public void ChangeLevel(String NextLevelName)
    {
        GetNode<GameEvents>("/root/GameEvents")
            .EmitSignal("LevelChanged", NextLevelName);
    }
}
