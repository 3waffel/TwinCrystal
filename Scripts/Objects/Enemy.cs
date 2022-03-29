using System;
using Godot;

public class Enemy : KinematicBody2D
{
    [Export]
    private float moveSpeed = 100f;

    [Export]
    private PackedScene _bulletScene = null;

    [Export]
    private Texture bulletTexture = null;

    [Export]
    private float bulletSpeed = 50f;

    [Export]
    private Node2D target = null;

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
        if (target != null)
        {
            Attack();
        }
    }

    public void Attack()
    {
        var bullet = (Bullet) _bulletScene.Instance();
        bullet.Position = Position;
        bullet.Target = target.GlobalPosition;
        bullet.Shooter = this;
        bullet.BulletTexture = bulletTexture;
        GetParent().AddChild(bullet);
    }

    public void OnDetectionAreaEntered(object body)
    {
        if (body is Player)
        {
            target = (Node2D) body;
        }
    }

    public void OnDetectionAreaExited(object body)
    {
        if (body is Player)
        {
            target = null;
        }
    }
}
