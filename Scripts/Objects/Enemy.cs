using System;
using Godot;

public class Enemy : KinematicBody2D
{
    [Export]
    private float _moveSpeed = 100f;

    [Export]
    private PackedScene _bulletScene = null;

    [Export]
    private Texture _bulletTexture = null;

    [Export]
    private float _bulletSpeed = 50f;

    [Export]
    private Node2D _target = null;

    public override void _Ready()
    {
        Area2D detectionArea = GetNode<Area2D>("DetectionArea");
        detectionArea
            .Connect("body_entered", this, nameof(OnDetectionAreaEntered));
        detectionArea
            .Connect("body_exited", this, nameof(OnDetectionAreaExited));
    }

    public override void _Process(float delta)
    {
        if (_target != null)
        {
            Attack();
        }
    }

    public void Attack()
    {
        var bullet = (Bullet) _bulletScene.Instance();
        bullet.Position = Position;
        bullet.Target = _target.GlobalPosition;
        bullet.Shooter = this;
        bullet.BulletTexture = _bulletTexture;
        bullet.Connect("BulletHit", this, nameof(OnBulletHit));
        GetParent().AddChild(bullet);
    }

    public void OnDetectionAreaEntered(object body)
    {
        if (body is Player)
        {
            _target = (Node2D) body;
        }
    }

    public void OnDetectionAreaExited(object body)
    {
        if (body is Player)
        {
            _target = null;
        }
    }

    public void OnBulletHit(Node2D bullet, Node2D target)
    {
        bullet.QueueFree();
    }
}
