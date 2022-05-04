using Godot;
using System;

public class GuardFollower : Follower
{
    [Export]
    private float _rotateSpeed = 30f;

    private Vector2 _rotateDirection = new Vector2(1f, 0f);
    
    private Vector2 _dashDirection = new Vector2(1f, 0f);

    private bool isDashing = false;

    private float dashSpeed = 100000f;

    private CircleShape2D _shape = new CircleShape2D();

    [Export]
    public float ShapeRadius { get { return _shape.Radius; } set { _shape.Radius = value; } }

    // TODO:
    private float deltaSum = 0f;

    public override void _Ready()
    {
        base._Ready();
        CollisionShape2D collisionShape = new CollisionShape2D();
        collisionShape.Shape = _shape;
        AddChild(collisionShape);
    }

    public override void _Process(float delta)
    {
        if (!_isFollowing)
        {
            return;
        }

        if (isDashing)
        {
            if (deltaSum > 0.5f)
            {
                deltaSum = 0;
                isDashing = false;
            }
            deltaSum += delta;
            MoveAndSlide(_dashDirection * dashSpeed * delta);
            return;
        }
        float rotateAngle = _rotateSpeed * delta;
        _rotateDirection = _rotateDirection.Rotated(rotateAngle);
        Position = _following.Position + _rotateDirection.Normalized() * _distance;

        if (Input.IsActionJustPressed("skill"))
        {
            Dash();
            isDashing = true;
        }
    }

    public void Dash()
    {
        Vector2 mousePosition = GetGlobalMousePosition();
        Vector2 direction = mousePosition - Position;
        GD.Print(direction);
        _dashDirection = direction.Normalized();

    }

}
