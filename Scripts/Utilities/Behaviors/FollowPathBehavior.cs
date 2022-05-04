using System;
using Godot;

public class FollowPathBehavior : Node2D
{
    private Vector2 velocity = Vector2.Zero;
    private bool valid = false;
    private float drag = 0.1f;
    GSLoader gsloader;

    private Godot.Object accel;
    private Godot.Object agent;
    private Godot.Object path;
    private Godot.Object follow;

    [Export]
    private float pathOffset = 20f;
    [Export]
    private float predictionTime = 0.3f;
    [Export]
    private float decelerationRadius = 100f;
    [Export]
    private float arrivalTolerance = 10f;
    [Export]
    private float linearAccelerationMax = 2000f;
    [Export]
    private float linearSpeedMax = 2000f;

    [Export]
    private Godot.Collections.Array<Vector2> pathPoints = new Godot.Collections.Array<Vector2>();

    public override void _Ready()
    {
        var body = GetParent<KinematicBody2D>();
        gsloader = GetNode<GSLoader>("/root/GSLoader");
        accel = (Godot.Object) gsloader.TargetAccelerationScript.New();
        agent = (Godot.Object) gsloader.KinematicBodyAgentScript.New(body);
        Vector3[] initPath =
        {
            new Vector3(GlobalPosition.x, GlobalPosition.y, 0),
            new Vector3(GlobalPosition.x, GlobalPosition.y, 0)
        };
        path = (Godot.Object) gsloader.PathScript.New(initPath, true);
        follow =
            (Godot.Object) gsloader.FollowPathScript.New(agent, path, 0, 0);

        follow.Set("path_offset", pathOffset);
        follow.Set("prediction_time", predictionTime);
        follow.Set("deceleration_radius", decelerationRadius);
        follow.Set("arrival_tolerance", arrivalTolerance);

        agent.Set("linear_acceleration_max", linearAccelerationMax);
        agent.Set("linear_speed_max", linearSpeedMax);
        agent.Set("linear_drag_percentage", drag);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (valid)
        {
            follow.Call("calculate_steering", accel);
            agent.Call("_apply_steering", accel, delta);
        }
    }

    public void OnPathEstablished(Godot.Collections.Array<Vector2> points)
    {
        var positions = new Godot.Collections.Array<Vector3>();
        foreach (var point in points)
        {
            positions.Add(new Vector3(point.x, point.y, 0));
        }
        path.Call("create_path", positions);
        valid = true;
    }
}
