using System;
using Godot;

public class Interactable : Area2D
{
    [Export]
    protected NodePath _interactionObjectPath;

    protected Node2D _interactionObject;

    public override void _Ready()
    {
        _interactionObject = GetNode<Node2D>(_interactionObjectPath);
        Connect("body_entered", this, nameof(OnBodyEntered));
    }

    public void OnBodyEntered(Node2D node)
    {
        if (node == _interactionObject)
        {
            GD.Print("Interactable: OnBodyEntered");
        }
    }
}
