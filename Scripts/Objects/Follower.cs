using System;
using Godot;

public class Follower : KinematicBody2D
{
    [Export]
    protected NodePath _followingPath;

    protected KinematicBody2D _following;

    [Export]
    protected float _distance = 50f;

    public override void _Ready()
    {
        _following = GetNode<KinematicBody2D>(_followingPath);
    }

    public override void _Process(float delta)
    {
        Vector2 moveTo = _following.Position - Position;
        Vector2 moveToNormalized = moveTo.Normalized();
        moveToNormalized *= _distance;
        moveTo -= moveToNormalized;
        MoveAndSlide (moveTo);
    }
}
