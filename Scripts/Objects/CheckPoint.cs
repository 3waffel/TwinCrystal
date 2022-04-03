using System;
using Godot;

public class CheckPoint : Interactable
{
    [Export]
    protected String _nextLevel;

    public override void _Ready()
    {
        base._Ready();
    }

    public void ShowCheckPointMenu()
    {
        if(_inArea)
        {
            if(Input.IsActionJustPressed("interact"))
            {
            }
        }
    }

    public void TransportPlayerToCheckPoint()
    {
        GetNode<GameEvents>("/root/GameEvents")
            .EmitSignal("LevelChanged", _nextLevel);
    }
}
