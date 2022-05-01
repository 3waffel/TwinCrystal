using System;
using Godot;

public class BulletFollower : Follower
{
    [Export]
    private PackedScene bulletScene;

    [Export]
    private Texture bulletTexture;

    [Export]
    private float bulletSpeed = 5f;

    [Export]
    private int bulletCount = 10;

    [Export]
    private float bulletLifeTime = 2f;

    [Export]
    private float bulletInterval = 0.5f;

    private Timer _timer;

    private bool _canShoot = true;

    private RandomNumberGenerator rng = new RandomNumberGenerator();

    public override void _Ready()
    {
        base._Ready();
        _timer = new Timer();
        AddChild (_timer);
        _timer.Connect("timeout", this, nameof(OnTimerTimeout));
        _timer.Start (bulletInterval);
        // GetNode<GameEvents>("/root/GameEvents")
        //     .Connect("BulletHitTarget", this, nameof(OnBulletHitTarget));
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (Input.IsActionJustPressed("skill") && _canShoot)
        {
            _canShoot = false;
            _timer.Start (bulletInterval);
            ShootBullet();
        }
    }

    public void ShootBullet()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            rng.Randomize();
            Vector2 bulletDirection =
                new Vector2(rng.RandfRange(-100f, 100f),
                    rng.RandfRange(-100f, 100f));
            bulletDirection.Normalized();
            bulletDirection *= bulletSpeed;
            Bullet bullet = (Bullet) bulletScene.Instance();
            bullet.Position = Position;
            bullet.Target = Position + bulletDirection;
            bullet.Shooter = this;
            bullet.LifeTime = bulletLifeTime;
            bullet.BulletTexture = bulletTexture;
            bullet.Connect("BulletHit", this, nameof(OnBulletHit));

            GetParent().AddChild(bullet);
        }
    }

    public void OnTimerTimeout()
    {
        _canShoot = true;
    }

    public void OnBulletHit(Node2D bullet, Node2D target)
    {
        if (target != _following)
        {
            if (target is Enemy)
            {
                (target as Enemy).Damage((bullet as Bullet).Damage);
            }
            bullet.QueueFree();
        }
    }
}
