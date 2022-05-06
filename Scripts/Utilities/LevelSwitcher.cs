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

    //TODO: quick cheat on player death
    Vector2 closestRespawnPoint = new Vector2();
    int rebirthLevelIndex = 0;

    public override void _Ready()
    {
        _viewportContainer = GetNode<ViewportContainer>("ViewportContainer");
        _viewport = _viewportContainer.GetNode<Viewport>("Viewport");
        _levelList.Resize(ScenesCount);

        GetViewport().Connect("size_changed", this, nameof(OnViewportResized));
        var gameEvents = GetNode<GameEvents>("/root/GameEvents");
        gameEvents.Connect("LevelChanged", this, nameof(OnLevelChanged));
        //TODO:
        gameEvents.Connect(nameof(GameEvents.PlayerDied), this, nameof(OnPlayerDied));

        gameEvents.Connect(nameof(GameEvents.GameStart), this, nameof(LoadStartLevel));
        closestRespawnPoint = _viewport.GetNode<Player>("Player").Position;

        // LoadStartLevel();
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
                // _levelList[door.ToLevelIndex] = nextLevelInstance;
                nextLevelInstance.LevelIndex = door.ToLevelIndex;
            }
            else
            {
                nextLevelInstance = _levelList[door.ToLevelIndex];
            }
            rebirthLevelIndex = door.ToLevelIndex;
        }
        else
        {
            return;
        }
        _viewport.AddChild (nextLevelInstance);
        var toDoor = nextLevelInstance.DoorIndexMap[(interactable as Door).ToDoorIndex];
        closestRespawnPoint = toDoor.Position;
        player.Position = closestRespawnPoint;
        player.Velocity = Vector2.Zero;
        camera.Position = player.Position;

        Task.Run( () => {
            Task.Delay(1000).Wait();
            _levelRootNode.QueueFree();
            player.Position = toDoor.Position;
            player.Velocity = Vector2.Zero;
            camera.Position = player.Position;
            _levelRootNode = nextLevelInstance;
        });
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

    public void LoadStartLevel()
    {
        _viewport.GetNode<Player>("Player").Position = closestRespawnPoint;
        _viewport.GetNode<Camera2D>("Camera").Position = closestRespawnPoint;
        
        _levelRootNode = _levelScenes[0].Instance() as Level;
        _levelList[0] = _levelRootNode;
        _viewport.AddChild(_levelRootNode);
    }

    //TODO:
    public void OnPlayerDied()
    {
        var player = _viewport.GetNode<Player>("Player");
        var camera = _viewport.GetNode<Camera2D>("Camera");
        player.Position = closestRespawnPoint;
        player.Velocity = Vector2.Zero;
        camera.Position = player.Position;
        player.Health = player.MaxHealth;
        
        Task.Run(() => {
            Task.Delay(1000).Wait();
            _levelRootNode.QueueFree();
            Level rebirthLevelInstance = _levelScenes[rebirthLevelIndex].Instance() as Level;
            rebirthLevelInstance.LevelIndex = rebirthLevelIndex;
            _viewport.AddChild(rebirthLevelInstance);
            _levelRootNode = rebirthLevelInstance;
            player.Position = closestRespawnPoint;
            player.Velocity = Vector2.Zero;
            camera.Position = player.Position;
        });
    }
}
