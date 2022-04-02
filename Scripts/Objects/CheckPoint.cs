using System;
using Godot;

public class CheckPoint : Interactable
{
    public override void _Ready()
    {
        base._Ready();
    }

    public void ShowCheckPointMenu()
    {
        if(_inArea)
        {
            if(Input.IsActionJustPressed("interact"))
            {
            }
        }
    }

    public void ReloadLevel()
    {
    }
}
