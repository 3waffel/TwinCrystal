using System;
using Godot;

public class SeekingEnemy : WanderingEnemy
{
    private SeekBehavior seekBehavior;
    public SeekBehavior SeekBehavior { get => seekBehavior; }

    public override void _Ready()
    {
        base._Ready();
        seekBehavior = new SeekBehavior();
        AddChild(seekBehavior);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        try {
            if (_target != null)
            {
                seekBehavior.PlayerAgent = _target.PlayerAgent.agent;
            }
            else
            {
                seekBehavior.PlayerAgent = null;
            }
        }
        catch
        {
            return;
        }
    }
}
