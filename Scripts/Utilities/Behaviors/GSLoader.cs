using Godot;
using System;

/// <summary>
/// This should be attached to autoload scene.
/// </summary>
public class GSLoader : Node
{
    [Export]
    private GDScript kinematicBodyAgentScript = ResourceLoader.Load<GDScript>("res://Libraries/com.gdquest.godot-steering-ai-framework/Agents/GSAIKinematicBody2DAgent.gd");
    public GDScript KinematicBodyAgentScript
    {
        get => kinematicBodyAgentScript;
    }
    [Export]
    private GDScript targetAccelerationScript = ResourceLoader.Load<GDScript>("res://Libraries/com.gdquest.godot-steering-ai-framework/GSAITargetAcceleration.gd");
    public GDScript TargetAccelerationScript
    {
        get => targetAccelerationScript;
    }
    [Export]
    private GDScript seekScript = ResourceLoader.Load<GDScript>("res://Libraries/com.gdquest.godot-steering-ai-framework/Behaviors/GSAISeek.gd");
    public GDScript SeekScript
    {
        get => seekScript;
    }
    [Export]
    private GDScript fleeScript = ResourceLoader.Load<GDScript>("res://Libraries/com.gdquest.godot-steering-ai-framework/Behaviors/GSAIFlee.gd");
    public GDScript FleeScript
    {
        get => fleeScript;
    }
    [Export]
    private GDScript agentLocationScript = ResourceLoader.Load<GDScript>("res://Libraries/com.gdquest.godot-steering-ai-framework/GSAIAgentLocation.gd");
    public GDScript AgentLocationScript
    {
        get => agentLocationScript;
    }
    [Export]
    private GDScript utilsScript = ResourceLoader.Load<GDScript>("res://Libraries/com.gdquest.godot-steering-ai-framework/GSAIUtils.gd");
    public GDScript UtilsScript
    {
        get => utilsScript;
    }

    public override void _Ready()
    {
        
    }
}
