using System;
using Godot;
using System.Collections.Generic;
using System.Linq;

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
    private int maximumJumps = 2;
    private int jumpsMade = 0;

    [Export]
    private float dashSpeed = 800f;
    [Export]
    private float dashInterval = 2f;
    [Export]
    private float dashDuration = 0.1f;
    private bool readyToDash = true;
    private bool isDashing = false;
    private Timer dashReadyTimer;
    private Timer dashStateTimer;
    Vector2 dashDirection = Vector2.Zero;

    private Vector2 _velocity = Vector2.Zero;
    public Vector2 Velocity
    {
        get => _velocity;
        set => _velocity = value;
    }

    [Export]
    private float invinciblityTime = 2f;
    private bool isInvincible = false;
    private Timer invinciblityTimer;

    [Export]
    private float maxHealth = 100f;
    private float _health = 100f;
    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            GetNode<GameEvents>("/root/GameEvents").EmitSignal(nameof(GameEvents.PlayerHealthChanged), _health, maxHealth);
        }
    }

    private Agent playerAgent;
    public Agent PlayerAgent
    {
        get => playerAgent;
    }

    public override void _Ready()
    {
        dashReadyTimer = new Timer();
        dashStateTimer = new Timer();
        dashReadyTimer.Connect("timeout", this, nameof(OnDashReady));
        dashStateTimer.Connect("timeout", this, nameof(OnDashEnd));
        AddChild(dashReadyTimer);
        AddChild(dashStateTimer);

        invinciblityTimer = new Timer();
        invinciblityTimer.Connect("timeout", this, nameof(OnInvinciblityEnd));
        AddChild(invinciblityTimer);

        GetNode<GameEvents>("/root/GameEvents").Connect(nameof(GameEvents.EnterCheckPoint), this, nameof(OnEnterCheckPoint));

        playerAgent = new Agent();
        AddChild(playerAgent);
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
        float verticalDirection =
            Input.GetActionStrength("move_down") -
            Input.GetActionStrength("move_up");
        
        dashDirection = new Vector2(
            horizontalDirection,
            verticalDirection
        );

        _velocity = isDashing ? dashDirection.Normalized() * dashSpeed :
            new Vector2(
                horizontalDirection * moveSpeed,
                _velocity.y + gravity * delta
            );

        if (readyToDash && Input.IsActionJustPressed("dash") && dashDirection != Vector2.Zero)
        {
            readyToDash = false;
            isDashing = true;
            dashReadyTimer.Start(dashInterval);
            dashStateTimer.Start(dashDuration);
        }        

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

    private void OnDashReady() => readyToDash = true;

    private void OnDashEnd() => isDashing = false;

    private void OnInvinciblityEnd() => isInvincible = false;

    public void Damage(float damage)
    {
        if (isInvincible)
        {
            return;
        }
        GD.Print("Player damaged");
        Health -= damage;
        isInvincible = true;
        invinciblityTimer.Start(invinciblityTime);
        
        if (Health <= 0f)
        {
            GD.Print("Player died");
            GetNode<GameEvents>("/root/GameEvents").EmitSignal(nameof(GameEvents.PlayerDied));
        }
    }

    private void OnEnterCheckPoint(CheckPoint checkPoint)
    {
        GD.Print("Player entered check point");
        Health = maxHealth;
    }
}
