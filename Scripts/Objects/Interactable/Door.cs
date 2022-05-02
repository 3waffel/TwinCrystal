using System;
using Godot;

public class Door : Interactable
{
    public String DoorName { get => "Door" + LevelIndex + "-" + DoorIndex; }

    /// <summary>
    /// The index of the door in the level. should be unique.
    /// </summary>
    [Export]
    private int _doorIndex = 0;
    public int DoorIndex
    {
        get => _doorIndex;
        set => _doorIndex = value;
    }

    /// <summary>
    /// The index of the door to which this door leads.
    /// </summary>
    [Export]
    private int _toDoorIndex = 0;
    public int ToDoorIndex
    {
        get => _toDoorIndex;
        set => _toDoorIndex = value;
    }

    public int LevelIndex { get => GetParent<Level>().LevelIndex;}

    [Export]
    private int _toLevelIndex = 0;
    public int ToLevelIndex
    {
        get => _toLevelIndex;
        set => _toLevelIndex = value;
    }
    
    private bool _isInitialized = false;
    private int initialzedIndex;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (_inArea)
        {
            if (Input.IsActionJustPressed("interact"))
            {
                GD.Print("Door: _Process");

                GetNode<GameEvents>("/root/GameEvents").EmitSignal("LevelChanged", this);

                // TODO: animation
                // GetNode<AnimationPlayer>("AnimationPlayer").Play("Open");
            }
        }
    }
    
    public void InitializeNextLevel()
    {
        // _tolevel = (Node2D) _nextLevel.Instance();
    }   
}
