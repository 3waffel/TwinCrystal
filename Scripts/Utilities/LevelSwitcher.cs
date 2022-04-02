using System;
using Godot;

public class LevelSwitcher : Node2D
{
    [Export]
    public String _currentLevelName;

    public Node2D _currentLevel;

    private ViewportContainer _viewportContainer;

    public override void _Ready()
    {
        _viewportContainer = GetNode<ViewportContainer>("ViewportContainer");
        GetViewport().Connect("size_changed", this, nameof(OnViewportResized));

        GetNode<GameEvents>("/root/GameEvents")
            .Connect("LevelChanged", this, nameof(OnLevelChanged));
    }

    /// <summary>
    /// Called when the current level is changed.
    /// </summary>
    /// <param name="NextLevelName"> The name of the level that will be changed to. </param>
    public void OnLevelChanged(String NextLevelName)
    {
        var level = (PackedScene) ResourceLoader.Load(NextLevelName);
        var levelInstance = (Node2D) level.Instance();
        AddChild (levelInstance);
        _currentLevel.QueueFree();
    }

    public void OnViewportResized()
    {
        _viewportContainer.SetSize(GetViewport().Size);
    }
}
