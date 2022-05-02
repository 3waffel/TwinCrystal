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

    [Signal]    
    public delegate void ConversationStarted(Conversable conversable);

    [Signal]
    public delegate void EnterCheckPoint(CheckPoint checkPoint);

    [Signal]
    public delegate void BubbleTriggered(int index);

    [Signal]
    public delegate void PlayerHealthChanged(float health, float maxHealth);

    [Signal]
    public delegate void PlayerDied();

    public override void _Ready()
    {
        
    }

}
