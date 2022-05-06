using Godot;
using System;

public class DirectBullet : Bullet
{
    private Vector2 _moveDirection;
    public Vector2 MoveDirection
    {
        get => _moveDirection;
        set => _moveDirection = value;
    }

    public override void _Ready()
    {
        base._Ready();
        if (Target == null)
        {
            QueueFree();
        }
        else
        {
            MoveDirection = Target - GlobalPosition;
            MoveDirection = MoveDirection.Normalized();
        }
    }

    public override void _Process(float delta)
    {
        Position += MoveDirection * MoveSpeed * delta;
    }

}
