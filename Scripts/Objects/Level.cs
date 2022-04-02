using Godot;
using System;

public class Level : Node2D
{
    [Export]
    public String _levelName;

    public override void _Ready()
    {
        GameEvents gameEvents = GetNode<GameEvents>("/root/GameEvents");
    }

    public void ChangeLevel(String NextLevelName)
    {
        GetNode<GameEvents>("/root/GameEvents").EmitSignal("LevelChanged", NextLevelName);
    }
}
