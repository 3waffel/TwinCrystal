using System;
using Godot;

public class Enemy : KinematicBody2D
{
    // TODO: specify what kind of bullet this enemy shoots
    [Export]
    protected PackedScene _bulletScene = null;
    [Export]
    protected Texture _bulletTexture = null;
    [Export]
    protected float _bulletSpeed = 50f;

    protected Player _target = null;

    [Export]
    protected float maxHealth = 100f;
    protected float _health;

    // TODO:
    private float deltaSum = 0f;

    [Export]
    private float detectionRadius = 200f;

    public override void _Ready()
    {
        _health = maxHealth;
        ChangeHealthBar(_health, maxHealth);
        CircleShape2D circleShape = new CircleShape2D();
        circleShape.Radius = detectionRadius;
        CollisionShape2D collisionShape = new CollisionShape2D();
        collisionShape.Shape = circleShape;

        Area2D detectionArea = new Area2D();
        detectionArea.AddChild(collisionShape);
        detectionArea
            .Connect("body_entered", this, nameof(OnDetectionAreaEntered));
        detectionArea
            .Connect("body_exited", this, nameof(OnDetectionAreaExited));
        AddChild(detectionArea);
    }

    public override void _Process(float delta)
    {
        if (_target != null && deltaSum > 0.5f)
        {
            deltaSum = 0;
            Attack();
        }
        deltaSum += delta;
    }

    public void Attack()
    {
        var bullet = (Bullet) _bulletScene.Instance();
        bullet.Position = Position;
        bullet.Target = _target.GlobalPosition;
        bullet.Shooter = this;
        if (_bulletTexture != null)
        {
            bullet.BulletTexture = _bulletTexture;
        }
        bullet.Connect("BulletHit", this, nameof(OnBulletHit));
        GetParent().AddChild(bullet);
    }

    public void OnDetectionAreaEntered(object body)
    {
        if (body is Player)
        {
            _target = body as Player;
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
        if (target is Player)
        {
            (target as Player).Damage((bullet as Bullet).Damage);
        }
    }

    public void Damage(float damage)
    {
        _health -= damage;
        ChangeHealthBar(_health, maxHealth);
        if (_health <= 0)
        {
            QueueFree();
        }
    }

    public void ChangeHealthBar(float health, float maxHealth)
    {
        var healthBar = GetNode<ProgressBar>("HealthBar");
        healthBar.MaxValue = maxHealth;
        healthBar.Value = health;
    }
}
