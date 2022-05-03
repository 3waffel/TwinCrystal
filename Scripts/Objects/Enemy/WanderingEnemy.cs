using Godot;
using System;

public class WanderingEnemy : Enemy
{
    [Export]
    private float _moveSpeed = 100f;

    private RandomNumberGenerator _rng = new RandomNumberGenerator();

    private Timer changeDirectionTimer = new Timer();

    private Vector2 _wanderDirection;

    public override void _Ready()
    {
        base._Ready();
        changeDirectionTimer.WaitTime = 1f;
        changeDirectionTimer.Connect("timeout", this, nameof(OnChangeDirectionTimerTimeout));
        AddChild(changeDirectionTimer);
        changeDirectionTimer.Start();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (_target == null)
        {
            Wander(delta);
        }
    }

    public void ChangeWanderDirection()
    {
        _wanderDirection = new Vector2(
            _rng.RandiRange(-1, 1),
            _rng.RandiRange(-1, 1)
        );
        _wanderDirection.Normalized();
    }

    public void Wander(float delta)
    {
        MoveAndCollide(_wanderDirection * _moveSpeed * delta);
    }

    public void OnChangeDirectionTimerTimeout()
    {
        ChangeWanderDirection();
        changeDirectionTimer.Start();
    }
}
