using Godot;
using System;

public class Camera : Camera2D
{
    [Export]
    private NodePath _trackObjectPath;

    private Node2D _trackObject;

    [Export]
    private float _cameraSpeed = 5f;

    public override void _Ready()
    {
        _trackObject = GetNode<Node2D>(_trackObjectPath);
    }

    public override void _Process(float delta)
    {
        Position = Position.LinearInterpolate(_trackObject.Position, delta * _cameraSpeed);
    }

}
