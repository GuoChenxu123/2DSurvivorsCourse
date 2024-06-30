using Godot;
using System;

public partial class basic_enemy : CharacterBody2D
{
    const float MAX_SPEED = 75.0f;

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
