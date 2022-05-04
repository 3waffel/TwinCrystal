using Godot;
using Godot.Collections;
using System;

public class ConversationPanel : Control
{
    private string _conversationName;

    private Array<string> _conversation;

    private Texture _portrait {
        get => GetNode<TextureRect>("Popup/Portrait").Texture;
        set => GetNode<TextureRect>("Popup/Portrait").Texture = value;
    }

    private String _conversationText {
        get => GetNode<Label>("Popup/Text").Text;
        set => GetNode<Label>("Popup/Text").Text = value;
    }

    private int _conversationIndex;

    public override void _Ready()
    {
        var gameEvents = GetNode<GameEvents>("/root/GameEvents");
        gameEvents.Connect(nameof(GameEvents.ConversationStarted), this, nameof(OnConversationStarted)); 
        gameEvents.Connect(nameof(GameEvents.PlayerDied), this, nameof(OnPlayerDied));
        GetNode<Button>("Popup/Next").Connect("pressed", this, nameof(OnNext));   
    }

    private void OnConversationStarted(Conversable conversable)
    {
        if (conversable != null)
        {
            _conversationIndex = 0;
            var popup = GetNode<Popup>("Popup");
            _conversationName = conversable.ConversationName;
            _conversation = conversable.Conversation;
            _conversationText = _conversation[0];
            _portrait = conversable.Portrait;
            popup.PopupCentered();
        }
    }

    private void OnNext()
    {
        if (_conversationIndex < _conversation.Count - 1)
        {
            _conversationIndex++;
            _conversationText = _conversation[_conversationIndex];
        }
        else
        {
            // GetNode<GameEvents>("/root/GameEvents").EmitSignal("ConversationEnded", _conversationName);
            GetNode<Popup>("Popup").Hide();
        }
    }

    // TODO: quick cheat
    private void OnPlayerDied()
    {
        _conversationIndex = 0;
        _conversationName = "LOL";
        _conversation = new Array<string>{"You Died.\n"};
        _conversationText = _conversation[0];
        _portrait = null;
        GetNode<Popup>("Popup").PopupCentered();
    }
}


