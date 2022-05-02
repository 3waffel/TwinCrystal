using Godot;
using System;

public class UI : Control
{

    private Control menuButton;
    private Control topLeftPanel;
    private Control topPanel;
    private Control bottomLeftPanel;
    private Control bottomPanel;
    private Control mapPanel;

    public override void _Ready()
    {
        menuButton = GetNode<Control>("CanvasLayer/MenuButton");
        topLeftPanel = GetNode<Control>("CanvasLayer/TopLeftPanel");
        topPanel = GetNode<Control>("CanvasLayer/TopPanel");
        bottomLeftPanel = GetNode<Control>("CanvasLayer/BottomLeftPanel");
        bottomPanel = GetNode<Control>("CanvasLayer/BottomPanel");
        mapPanel = GetNode<Control>("CanvasLayer/MapPanel");

        var gameEvents = GetNode<GameEvents>("/root/GameEvents");

        gameEvents.Connect(nameof(GameEvents.PlayerHealthChanged), this, nameof(OnPlayerHealthChanged));
        gameEvents.Connect(nameof(GameEvents.EnterCheckPoint), this, nameof(OnEnterCheckPoint));
        // deal with signal in editor
    }

    private void OnBottomPanelButtonPressed()
    {
        mapPanel.Visible = !mapPanel.Visible;
    }

    private void OnPlayerHealthChanged(float health, float maxHealth)
    {
        var healthBar = topLeftPanel.GetNode<ProgressBar>("HealthBar");
        healthBar.MaxValue = maxHealth;
        healthBar.Value = health;
    }

    private void OnEnterCheckPoint(CheckPoint checkPoint)
    {
        // var map = mapPanel.GetNode<Map>("Map");
        // map.SetCheckPoint(checkPoint);
    }
}


