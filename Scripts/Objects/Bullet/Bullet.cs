using Godot;
using System;

public class Bullet : Area2D
{
    [Signal]
    public delegate void BulletHit(Node2D Bullet, Node2D Target);

    public Texture BulletTexture { get => GetNode<Sprite>("Sprite").Texture; set => GetNode<Sprite>("Sprite").Texture = value; }

    [Export]
    private float damage = 10f;
    public float Damage { get => damage; set => damage = value; }

    [Export]
    private float moveSpeed = 5f;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    [Export]
    private float lifeTime = 5f;
    public float LifeTime { get => lifeTime; set => lifeTime = value; }

    private Timer lifeTimer;

    [Export]
    private Vector2 _target;
    public Vector2 Target
    {
        get => _target;
        set => _target = value;
    }

    [Export]
    private Node2D _shooter;
    public Node2D Shooter
    {
        get => _shooter;
        set => _shooter = value;
    }

    public override void _Ready()
    {
        lifeTimer = GetNode<Timer>("LifeTimer");
        lifeTimer.WaitTime = lifeTime;
        lifeTimer.Connect("timeout", this, nameof(OnLifeTimerTimeout));
        lifeTimer.Start();

        Connect("body_entered", this, nameof(OnBodyEntered));
    }

    public override void _Process(float delta)
    {
        if (Target == null)
        {
            QueueFree();
        }
        else
        {
            Vector2 direction = Target - GlobalPosition;
            direction.Normalized();
            Position += direction * moveSpeed * delta;
        }
    }

    public void OnBodyEntered(Node2D target)
    {
        if (target != Shooter)
        {
            EmitSignal(nameof(BulletHit), this, target);
        }
    }

    public void OnLifeTimerTimeout()
    {
        QueueFree();
    }
}
