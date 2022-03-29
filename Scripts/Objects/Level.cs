using Godot;
using System;

public class Level : Node2D
{
    [Export]
    public String _levelName;

    public override void _Ready()
    {

    }

    public void ChangeLevel(String NextLevelName)
    {
        EmitSignal("LevelChanged", NextLevelName);
    }
}
