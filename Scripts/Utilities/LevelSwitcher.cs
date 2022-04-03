using System;
using Godot;

public class LevelSwitcher : Node2D
{
    [Export]
    private NodePath _levelRoot;

    private Node2D _levelRootNode;

    private ViewportContainer _viewportContainer;

    private Viewport _viewport;

    public override void _Ready()
    {
        _viewportContainer = GetNode<ViewportContainer>("ViewportContainer");
        _viewport = _viewportContainer.GetNode<Viewport>("Viewport");
        _levelRootNode = GetNode<Node2D>(_levelRoot);

        GetViewport().Connect("size_changed", this, nameof(OnViewportResized));
        GetNode<GameEvents>("/root/GameEvents")
            .Connect("LevelChanged", this, nameof(OnLevelChanged));
    }

    /// <summary>
    /// Called when the current level is changed.
    /// </summary>
    /// <param name="NextLevelName"> The name of the level that will be changed to. </param>
    public void OnLevelChanged(PackedScene nextLevel)
    {
        _viewport.RemoveChild (_levelRootNode);
        // PackedScene packedScene = (PackedScene) new PackedScene();
        // packedScene.Pack (_levelRootNode);
        // ResourceSaver
        //     .Save("res://Scenes/Levels/" + (_levelRootNode as Level).LevelName + "_tmp.tscn",
        //     packedScene);

        Node2D nextLevelInstance = (Node2D) nextLevel.Instance();
        
        (TileMap tileMap, Vector2 spawnPoint) = TilesGeneration.TilesGen();
        tileMap.Position = _viewport.GetNode<Node2D>("Player").Position - spawnPoint;
        nextLevelInstance.AddChild(tileMap);

        _viewport.AddChild (nextLevelInstance);
        _levelRootNode = nextLevelInstance;
    }

    public void OnViewportResized()
    {
        _viewportContainer.SetSize(GetViewport().Size);
    }
}
