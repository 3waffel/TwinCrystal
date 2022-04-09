using System;
using System.Threading;
using System.Threading.Tasks;
using Godot;

public class LevelSwitcher : Node2D
{
    [Export]
    private NodePath _levelRoot;

    private Node2D _levelRootNode;

    private ViewportContainer _viewportContainer;

    private Viewport _viewport;

    private Godot.Collections.Array _levelList;

    public override void _Ready()
    {
        _viewportContainer = GetNode<ViewportContainer>("ViewportContainer");
        _viewport = _viewportContainer.GetNode<Viewport>("Viewport");
        _levelRootNode = GetNode<Node2D>(_levelRoot);
        _levelList = new Godot.Collections.Array();

        GetViewport().Connect("size_changed", this, nameof(OnViewportResized));
        GetNode<GameEvents>("/root/GameEvents")
            .Connect("LevelChanged", this, nameof(OnLevelChanged));
    }

    public void OnLevelChanged(Interactable interactable)
    {
        Node2D nextLevelInstance;
        if ((interactable as Door).NextLevel != null)
        {
            PackedScene nextLevel = (interactable as Door).NextLevel;
            nextLevelInstance = (Node2D) nextLevel.Instance();
        }
        else if (_levelList.Count >= 1)
        {
            nextLevelInstance = (Node2D) _levelList[_levelList.Count - 1];
            _levelList.RemoveAt(_levelList.Count - 1);
        }
        else
        {
            GD.Print("No more levels to load");
            return;
        }
        var player = _viewport.GetNode<Player>("Player");
        player.Velocity = Vector2.Zero;
        player.Position = nextLevelInstance.Position;
        _levelList.Add (_levelRootNode);
        _viewport.RemoveChild (_levelRootNode);
        _viewport.AddChild (nextLevelInstance);
        _levelRootNode = nextLevelInstance;
    }

    public void OnViewportResized()
    {
        _viewportContainer.SetSize(GetViewport().Size);
    }

    public void SaveLevelAsPackedScene()
    {
        PackedScene packedScene = new PackedScene();
        packedScene.Pack (_levelRootNode);
        ResourceSaver
            .Save("user://Scenes/Levels/" +
            (_levelRootNode as Level).LevelName +
            "_tmp.tscn",
            packedScene);
    }
}
