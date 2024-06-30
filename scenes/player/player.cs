using Godot;
using System;

public partial class player : CharacterBody2D
{
    const float MAX_SPEED = 200.0f;

    public override void _Process(double delta)
    {
        Vector2 vector2 = GetMovementVector();
        Vector2 direction = vector2.Normalized();
        Velocity = MAX_SPEED * direction;
        MoveAndSlide();
    }

    public override void _Ready()
    {
        base._Ready();
    }

    public Vector2 GetMovementVector()
    {
        float x_movement = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        float y_movement = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        return new Vector2(x_movement, y_movement);


    }
}
