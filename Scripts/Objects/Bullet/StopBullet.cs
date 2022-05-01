using Godot;
using System;

public class StopBullet : Bullet
{
    public override void _Ready()
    {
        base._Ready();
        if (Target == null)
        {
            QueueFree();
        }
    }

    public override void _Process(float delta)
    {
        Vector2 direction = Target - GlobalPosition;
        direction.Normalized();
        Position += direction * MoveSpeed * delta;
    }
}
