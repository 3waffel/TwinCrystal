using System;
using Godot;

public class Interactable : Area2D
{
    [Export]
    protected NodePath _interactionObjectPath;

    protected Node2D _interactionObject;

    protected bool _inArea = false;

    public override void _Ready()
    {
        _interactionObject = GetNode<Node2D>(_interactionObjectPath);
        Connect("body_entered", this, nameof(OnBodyEntered));
        Connect("body_exited", this, nameof(OnBodyExited));
    }

    public virtual void OnBodyEntered(Node2D node)
    {
        if (node == _interactionObject)
        {
            GD.Print("Interactable: OnBodyEntered");
            _inArea = true;
        }
    }

    public virtual void OnBodyExited(Node2D node)
    {
        if (node == _interactionObject)
        {
            GD.Print("Interactable: OnBodyExited");
            _inArea = false;
        }
    }
}
