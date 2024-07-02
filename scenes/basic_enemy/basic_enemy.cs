using Godot;
using System;

public partial class basic_enemy : CharacterBody2D
{
    const float MAX_SPEED = 75.0f;

    public override void _Ready()
    {
        Area2D area2D = GetNode<Area2D>("Area2D");
        area2D.AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        QueueFree();
    }

    public override void _Process(double delta)
    {
        Vector2 direction = GetDirectionToPlayer();
        Velocity = direction * MAX_SPEED;
        MoveAndSlide();
    }

    public Vector2 GetDirectionToPlayer()
    {
        Node2D player_node = GetTree().GetFirstNodeInGroup("player") as Node2D;
        if (player_node != null)
        {
            return (player_node.GlobalPosition - GlobalPosition).Normalized();
        }
        return Vector2.Zero;
    }
}
