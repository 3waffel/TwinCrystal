using Godot;
using System;

public class PathDrawer : Node2D
{
    [Signal]
    public delegate void PathEstablished(Godot.Collections.Array<Vector2> pathPoints);

    private Godot.Collections.Array<Vector2> activePoints = new Godot.Collections.Array<Vector2>(); 

    private bool isDrawing = false;

    private float distanceThreshold = 100f;

    public override void _Ready()
    {
        
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            if (isDrawing)
            {
                activePoints.Add(((InputEventMouseMotion)@event).Position);
                Update();
            }
        }
        else if (@event is InputEventMouseButton)
        {
            if ((@event as InputEventMouseButton).Pressed && (@event as InputEventMouseButton).ButtonIndex == (int)ButtonList.Left)
            {
                activePoints.Clear();
                activePoints.Add(((InputEventMouseButton)@event).Position);
                isDrawing = true;
                Update();
            }
            else if (!(@event as InputEventMouseButton).Pressed)
            {
                isDrawing = false;
                if (activePoints.Count > 1)
                {
                    //
                }
            }
        }
    }

    public override void _Draw()
    {
        if (isDrawing)
        {
            foreach (var point in activePoints)
            {
                DrawCircle(point, 2f, new Color(0, 0, 0, 0.5f));
            }
        }
        else if (activePoints.Count > 0)
        {
            DrawCircle(activePoints[0], 2f, new Color(0, 0, 0, 0.5f));
            DrawCircle(activePoints[activePoints.Count - 1], 2f, new Color(0, 0, 0, 0.5f));
            Godot.Vector2[] points = new Godot.Vector2[activePoints.Count];
            for (int i = 0; i < activePoints.Count; i++)
            {
                points[i] = activePoints[i];
            }
            DrawPolyline(points, new Color(0, 0, 0, 0.5f));
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
}
