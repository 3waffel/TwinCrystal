using System;
using Godot;

public class Player : KinematicBody2D
{
    [Export]
    private float gravity = 1000f;

    [Export]
    private float moveSpeed = 400f;

    [Export]
    private float jumpStrength = 400f;

    [Export]
    private float doubleJumpStrength = 400f;

    [Export]
    private int maximumJumps = OS.IsDebugBuild()? 100 : 2;

    private int jumpsMade = 0;

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
        bool isFalling = _velocity.y != 0f && !IsOnFloor();
        bool isJumping = Input.IsActionJustPressed("jump") && IsOnFloor();
        bool isDoubleJumping = Input.IsActionJustPressed("jump") && isFalling;
        bool isIdling = IsOnFloor() && _velocity.x == 0f;
        bool isRunning = IsOnFloor() && _velocity.x != 0f;

        float horizontalDirection =
            Input.GetActionStrength("move_right") -
            Input.GetActionStrength("move_left");
        _velocity.x = horizontalDirection * moveSpeed;
        _velocity.y += gravity * delta;


        if (isJumping)
        {
            jumpsMade++;
            _velocity.y = -jumpStrength;
        }
        else if (isDoubleJumping)
        {
            jumpsMade++;
            if (jumpsMade <= maximumJumps)
            {
                _velocity.y = -doubleJumpStrength;
            }
        }
        else if (isIdling || isRunning)
        {
            jumpsMade = 0;
        }

        _velocity = MoveAndSlide(_velocity, Vector2.Up);
    }
}
