using System;
using Godot;

/// <summary>
/// The parent should be a KinematicBody2D
/// The movement should be controlled by SeekBehavior
/// </summary>
public class SeekBehavior : Node2D
{
    private Vector2 velocity = Vector2.Zero;
    [Export]
    private float startSpeed = 200f;
    [Export]
    private float startAccel = 1000f;
    [Export]
    private bool useSeek = true;

    private Godot.Object playerAgent;
    public Godot.Object PlayerAgent
    {
        get => playerAgent;
        set => playerAgent = value;
    }

    GSLoader gsLoader;
    private Godot.Object agent;
    private Godot.Object accel;
    private Godot.Object seek;
    private Godot.Object flee;

    // TODO: quick cheat
    private bool isPlayerAgentInitialized = false;

    public override void _Ready()
    {
        var body = GetParent<KinematicBody2D>();
        gsLoader = GetNode<GSLoader>("/root/GSLoader");

        agent = (Godot.Object) gsLoader.KinematicBodyAgentScript.New(body);
        accel = (Godot.Object) gsLoader.TargetAccelerationScript.New();

        agent.Set("linear_acceleration_max", startAccel);
        agent.Set("linear_speed_max", startSpeed);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (playerAgent == null)
        {
            return;
        }
        else if (!isPlayerAgentInitialized)
        {
            seek = (Godot.Object) gsLoader.SeekScript.New(agent, playerAgent);
            flee = (Godot.Object) gsLoader.FleeScript.New(agent, playerAgent);
            isPlayerAgentInitialized = true;
        }
        else
        {
            if (useSeek)
            {
                seek.Call("calculate_steering", accel);
            }
            else
            {
                flee.Call("calculate_steering", accel);
            }
        }

        agent.Call("_apply_steering", accel, delta);
    }
}
