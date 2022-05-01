using System;
using Godot;

public class Door : Interactable
{
    [Export]
    protected PackedScene _nextLevel;

    public PackedScene NextLevel { get => _nextLevel; set => _nextLevel = value; }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (_inArea)
        {
            if (Input.IsActionJustPressed("interact"))
            {
                GD.Print("Door: _Process");
                // GetNode<AnimationPlayer>("AnimationPlayer").Play("Open");
                GetNode<GameEvents>("/root/GameEvents").EmitSignal("LevelChanged", this);
            }
        }
    }
}
