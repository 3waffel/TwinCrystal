using Godot;
using System;

public class PathCreator : Area2D
{
    [Signal]
    public delegate void PathEstablished(Godot.Collections.Array<Vector2> pathPoints);

    private Godot.Collections.Array<Vector2> activePoints = new Godot.Collections.Array<Vector2>(); 

    private float distanceThreshold = 100f;

    private Timer createPathTimer = new Timer();
    
    private bool canCreatePath = false;

    [Export]
    private float createPathInterval = 10f;

    [Export]
    private Godot.Collections.Array<Curve2D> curves = new Godot.Collections.Array<Curve2D>();
    private int currentCurve = 0;

    public override void _Ready()
    {
        createPathTimer.Connect("timeout", this, nameof(CreatePathFromNodes));
        AddChild(createPathTimer);
        createPathTimer.Start(createPathInterval);

        Connect("body_entered", this, nameof(OnBodyEntered));
        Connect("body_exited", this, nameof(OnBodyExited));

        foreach (var child in GetChildren())
        {
            if (child is Path2D)
            {
                var path = (Path2D) child;
                curves.Add(path.Curve);
            }
        }
    }

    private void Simplify()
    {
        Vector2 first = activePoints[0];
        Vector2 last = activePoints[activePoints.Count - 1];
        var key = first;
        var simplifiedPath = new Godot.Collections.Array<Vector2>{first};
        for (int i = 1; i < activePoints.Count; i++)
        {
            Vector2 point = activePoints[i];
            var distance = point.DistanceTo(key);
            if (distance > distanceThreshold)
            {
                key = point;
                simplifiedPath.Add(point);
            }
        }
        activePoints = simplifiedPath;
        if (activePoints[activePoints.Count - 1] != last)
        {
            activePoints.Add(last);
        }
        Update();
        EmitSignal(nameof(PathEstablished), activePoints);
    }

    private void CreatePathFromNodes()
    {
        if (canCreatePath)
        {
            if (curves.Count > 0)
            {
                currentCurve = (currentCurve + 1) % curves.Count;
                EmitSignal(nameof(PathEstablished), curves[currentCurve].GetBakedPoints());
            }
        }
        createPathTimer.Start(createPathInterval);
    }

    private void OnBodyEntered(Node2D body)
    {
        var children = body.GetChildren();
        FollowPathBehavior behavior = null;
        foreach (var child in children)
        {
            if (child is FollowPathBehavior)
            {
                behavior = child as FollowPathBehavior;
                break;
            }
        }
        if (behavior != null)
        {
            Connect(nameof(PathEstablished), behavior, nameof(FollowPathBehavior.OnPathEstablished));
            canCreatePath = true;
        }
    }

    private void OnBodyExited(Node2D body)
    {  
        var children = body.GetChildren();
        FollowPathBehavior behavior = null;
        foreach (var child in children)
        {
            if (child is FollowPathBehavior)
            {
                behavior = child as FollowPathBehavior;
                break;
            }
        }
        if (behavior != null)
        {
            Disconnect(nameof(PathEstablished), behavior, nameof(FollowPathBehavior.OnPathEstablished));
            canCreatePath = false;
        }
    }
}
