using System;
using Godot;

public class LevelSwitcher : Node2D
{
    [Export]
    public String _currentLevelName;

    public Node2D _currentLevel;

    public override void _Ready()
    {
        _currentLevel.Connect("LevelChanged", this, nameof(OnLevelChanged));
    }

    /// <summary>
    /// Called when the current level is changed.
    /// </summary>
    /// <param name="NextLevelName"> The name of the level that will be changed to. </param>
    public void OnLevelChanged(String NextLevelName)
    {
        var level = (PackedScene) ResourceLoader.Load(NextLevelName);
        var levelInstance = (Node2D) level.Instance();
        AddChild(levelInstance);
        levelInstance.Connect("LevelChanged", this, nameof(OnLevelChanged));
        _currentLevel.QueueFree();
    }
}