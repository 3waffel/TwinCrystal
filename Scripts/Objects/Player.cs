using System;
using Godot;

public class Player : KinematicBody2D
{
    [Export]
    private float moveSpeed = 400f;

    [Export]
    private float jumpSpeed = 700f;

    [Export]
    private float gravity = 1000f;

    private Vector2 _velocity = Vector2.Zero;
    public Vector2 Velocity
    {
        get => _velocity;
        set => _velocity = value;
    }

    public override void _Ready()
    {
    }

    public override void _Process(float delta)
    {
    }

    public override void _PhysicsProcess(float delta)
    {
        float horizontalDirection =
            Input.GetActionStrength("move_right") -
            Input.GetActionStrength("move_left");
        _velocity.x = horizontalDirection * moveSpeed;
        _velocity.y += gravity * delta;

        if (Input.IsActionJustPressed("jump"))
        {
            _velocity.y = -jumpSpeed;
        }

        _velocity = MoveAndSlide(_velocity, Vector2.Up);
    }
}
