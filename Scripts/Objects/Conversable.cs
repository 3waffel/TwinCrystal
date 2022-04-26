using Godot;
using System;
using Godot.Collections;

public class Conversable : Interactable
{
    [Export]
    private string _conversationName;
    public string ConversationName => _conversationName;

    [Export]
    private Array<string> _conversation;
    public Array<string> Conversation => _conversation;

    [Export]
    private Texture _portrait;
    public Texture Portrait => _portrait;   

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (_inArea)
        {
            if (Input.IsActionJustPressed("interact"))
            {
                GetNode<GameEvents>("/root/GameEvents").EmitSignal("ConversationStarted", this);
            }
        }
    }

    private void ShowConversationPanel()
    {
        // var conversationPanel = (ConversationPanel)_conversationPanel.Instance();
        // conversationPanel.SetConversation(_conversationName, _conversation);
        // GetTree().Root.AddChild(conversationPanel);
    }
}
