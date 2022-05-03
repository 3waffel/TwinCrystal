using Godot;
using System;

public class Agent : Node2D
{
    GSLoader gsLoader;
    public Godot.Object agent;
    private Godot.Object utils;

    public override void _Ready()
    {
        gsLoader = GetNode<GSLoader>("/root/GSLoader");
        agent = (Godot.Object)gsLoader.AgentLocationScript.New();
        utils = (Godot.Object)gsLoader.UtilsScript.New();
        var position = utils.Call("to_vector3", GlobalPosition);
        agent.Set("position", position);
    }

    public override void _PhysicsProcess(float delta)
    {
        var position = utils.Call("to_vector3", GlobalPosition);
        agent.Set("position", position);
    }
}
