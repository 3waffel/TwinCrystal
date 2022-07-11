using Godot;
using System;

public class Dice : Node
{
    public static int sides = 20;

    public override void _Ready()
    {
        
    }

    public static int Roll(int times)
    {
        int result = 0;
        for (int i = 0; i < times; i++)
        {
            result += (int)GD.RandRange(1, sides);
        }
        return result;
    }

}
