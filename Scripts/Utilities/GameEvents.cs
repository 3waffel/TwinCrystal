using Godot;
using System;

public class GameEvents : Node
{
    public int _currentScene = 0;
    
    // [Signal]
    // public delegate void GameOver();

    [Signal]
    public delegate void BulletHitTarget(Node2D target);

    [Signal]
    public delegate void LevelChanged(Interactable interactable);

    public override void _Ready()
    {
        
    }

}
