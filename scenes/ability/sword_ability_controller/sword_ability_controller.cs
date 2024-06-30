 using Godot;
using System;

public partial class sword_ability_controller : Node
{
	[Export]
	private PackedScene sword_ability;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnTimerTimeout()
	{
		Node2D player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null)
		{
			return;
		}
		Node2D sword_instance = sword_ability.Instantiate() as Node2D;
		player.GetParent().AddChild(sword_instance);
		sword_instance.GlobalPosition = player.GlobalPosition;
		GD.Print("Sword ability activated!");
    }
}
