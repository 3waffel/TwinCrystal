using System;
using Godot;

public class Summoned : KinematicBody2D
{
    [Export]
    private NodePath _summonerPath;

    private KinematicBody2D _summoner;

    [Export]
    private Vector2 _offset;

    public override void _Ready()
    {
        _summoner = GetNode<KinematicBody2D>(_summonerPath);
    }

    public override void _Process(float delta)
    {
        Vector2 moveTo = _summoner.Position - Position + _offset;
        MoveAndSlide (moveTo);
    }

    
}
