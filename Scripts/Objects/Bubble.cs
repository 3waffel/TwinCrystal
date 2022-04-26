using Godot;
using Godot.Collections;
using System;

/// <summary>
/// Conversation Bubble, should be attached to an object.
/// The dialog will be triggered by a signal.
/// The bubble defaultly contains the conversation text.
/// </summary>
public class Bubble : Node
{
    [Export]
    private Resource _conversation;

    [Export]
    private Array<Array<String>> _conversationList;

    [Export]
    private int _bubbleIndex;

    [Export]
    private int _conversationIndex;

    [Export]
    private bool _isVisible = false;

    public override void _Ready()
    {
        GetNode<GameEvents>("/root/GameEvents").Connect("BubbleTriggered", this, nameof(OnBubbleTriggered));
    }

    public override void _Process(float delta)
    {
        if (_isVisible)
        {
            GetNode<Label>("Bubble/Label").Text = _conversationList[_bubbleIndex][_conversationIndex];
            // GetNode<Button>("Bubble/Button").Text = "Next";
        }
    }

    public void OnBubbleTriggered(int bubbleIndex)
    {
        if (bubbleIndex == _bubbleIndex)
        {
            if (_conversationList[_conversationIndex].Count > 1)
            {
                _conversationList[_conversationIndex].RemoveAt(0);
                GetNode<Label>("Text").Text = _conversationList[_conversationIndex][0];
            }
            else
            {
                // GetNode<GameEvents>("/root/GameEvents").EmitSignal("ConversationEnded");
                // QueueFree();
            }
        }
    }

}
