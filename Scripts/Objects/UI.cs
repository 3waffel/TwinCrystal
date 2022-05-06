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
    private Control startMenu;

    public override void _Ready()
    {
        menuButton = GetNode<MenuButton>("CanvasLayer/MenuButton");
        topLeftPanel = GetNode<Control>("CanvasLayer/TopLeftPanel");
        topPanel = GetNode<Control>("CanvasLayer/TopPanel");
        bottomLeftPanel = GetNode<Control>("CanvasLayer/BottomLeftPanel");
        bottomPanel = GetNode<Control>("CanvasLayer/BottomPanel");
        mapPanel = GetNode<Control>("CanvasLayer/MapPanel");
        startMenu = GetNode<Control>("CanvasLayer/StartMenu");

        startMenu.GetNode<Button>("Panel/Button").Connect("pressed", this, nameof(StartGame));

        var gameEvents = GetNode<GameEvents>("/root/GameEvents");

        gameEvents.Connect(nameof(GameEvents.PlayerHealthChanged), this, nameof(OnPlayerHealthChanged));
        gameEvents.Connect(nameof(GameEvents.EnterCheckPoint), this, nameof(OnEnterCheckPoint));
        gameEvents.Connect(nameof(GameEvents.LevelChanged), this, nameof(OnLevelChanged));
        // deal with signal in editor
    }

    private void StartGame()
    {
        GetNode<GameEvents>("/root/GameEvents").EmitSignal(nameof(GameEvents.GameStart));
        startMenu.Visible = false;
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

    private void OnLevelChanged(Interactable interactable)
    {
        var graphEdit = mapPanel.GetNode<GraphEdit>("GraphEdit");
        var levelList = GetParent().GetParent<LevelSwitcher>().LevelList;

        foreach (var node in graphEdit.GetChildren())
        {
            if (node is GraphNode)
                (node as GraphNode).QueueFree();       
        }

        Vector2 position = new Vector2(0, 0);

        foreach (var level in levelList)
        {
            if (level == null)
            {
                continue;
            }
            var graphNode = new GraphNode();
            graphNode.Set("title", level.LevelName);
            graphNode.Set("levelIndex", level.LevelIndex);
            graphEdit.AddChild(graphNode);
            graphNode.RectPosition = position;
            position += new Vector2(0, 50);
        }
    }
}


