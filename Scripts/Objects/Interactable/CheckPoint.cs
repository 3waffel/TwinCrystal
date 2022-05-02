using System;
using Godot;

public class CheckPoint : Interactable
{
    private Node2D _currentLevel;
    public Node2D CurrentLevel
    {
        get => _currentLevel;
    }

    public override void _Ready()
    {
        base._Ready();
        _currentLevel = GetParent<Node2D>();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if(_inArea)
        {
            if(Input.IsActionJustPressed("interact"))
            {
                GetNode<GameEvents>("/root/GameEvents").EmitSignal(nameof(GameEvents.EnterCheckPoint), this);
                // TODO: play animation and audio
            }
        }
    }

    // TODO
    public void TransportPlayerToCheckPoint()
    {
        GetNode<GameEvents>("/root/GameEvents")
            .EmitSignal(nameof(GameEvents.LevelChanged), this);
    }

}
