using System;
using Godot;

public class Interactable : Area2D
{
    protected bool _inArea = false;

    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnBodyEntered));
        Connect("body_exited", this, nameof(OnBodyExited));
    }

    public virtual void OnBodyEntered(Node2D node)
    {
        if (node is Player)
        {
            _inArea = true;
        }
    }

    public virtual void OnBodyExited(Node2D node)
    {
        if (node is Player)
        {
            _inArea = false;
        }
    }
}
