using System;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using System.Collections.Generic;

public class LevelSwitcher : Node2D
{
    private Level _levelRootNode;

    private ViewportContainer _viewportContainer;
    private Viewport _viewport;

    [Export]
    private Godot.Collections.Array<PackedScene> _levelScenes = new Godot.Collections.Array<PackedScene>();
    private Godot.Collections.Array<Level> _levelList = new Godot.Collections.Array<Level>();
    public Godot.Collections.Array<Level> LevelList { get => _levelList; }
    public int ScenesCount { get => _levelScenes.Count; }

    public override void _Ready()
    {
        _viewportContainer = GetNode<ViewportContainer>("ViewportContainer");
        _viewport = _viewportContainer.GetNode<Viewport>("Viewport");
        _levelList.Resize(ScenesCount);


        GetViewport().Connect("size_changed", this, nameof(OnViewportResized));
        GetNode<GameEvents>("/root/GameEvents")
            .Connect("LevelChanged", this, nameof(OnLevelChanged));

        _levelRootNode = _levelScenes[0].Instance() as Level;
        _levelList[0] = _levelRootNode;
        _viewport.AddChild(_levelRootNode);
    }

    public void OnLevelChanged(Interactable interactable)
    {
        Level nextLevelInstance;
        var player = _viewport.GetNode<Player>("Player");
        var camera = _viewport.GetNode<Camera2D>("Camera");
        if (interactable is Door)
        {
            var door = interactable as Door;
            // TODO
            if (_levelList[door.ToLevelIndex] == null) 
            {
                nextLevelInstance = _levelScenes[door.ToLevelIndex].Instance() as Level;
                _levelList[door.ToLevelIndex] = nextLevelInstance;
                nextLevelInstance.LevelIndex = door.ToLevelIndex;
                GD.Print("New Level: " + nextLevelInstance.LevelName);
            }
            else
            {
                nextLevelInstance = _levelList[door.ToLevelIndex];
                GD.Print("Existing Level: " + nextLevelInstance.LevelName);
            }
        }
        else
        {
            GD.Print("LevelSwitcher: OnLevelChanged: Unknown interactable type");
            return;
        }
        _viewport.RemoveChild (_levelRootNode);
        _viewport.AddChild (nextLevelInstance);
        _levelRootNode = nextLevelInstance;

        var toDoor = nextLevelInstance.DoorIndexMap[(interactable as Door).ToDoorIndex];
        player.Position = toDoor.Position;
        player.Velocity = Vector2.Zero;
        camera.Position = player.Position;

        GD.Print(_levelList.Count);
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
