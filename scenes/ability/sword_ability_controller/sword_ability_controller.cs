 using Godot;
using System;
using System.Linq;

public partial class sword_ability_controller : Node
{
	[Export]
	private PackedScene sword_ability;
	private const float MAX_RANGE = 150.0f;
    private Random random = new Random();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//确保sword_ability不为空
		if(sword_ability == null){
			GD.PrintErr("sword_ability is null");
			return;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnTimerTimeout()
    {
        // 获取玩家节点
        Node2D player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null)
		{
            GD.PrintErr("Player not found.");
            return;
		}
        // 获取所有在 "enemy" 组中的节点
        Godot.Collections.Array<Node> enemies = GetTree().GetNodesInGroup("enemy");
        if (enemies.Count == 0)
        {
            GD.Print("No enemies found.");
            return;
        }
        // 过滤敌人
        // 使用LINQ的Where方法来过滤敌人，并将其转换为列表。Cast<Node2D>() 将 Node 类型的数组转换为 Node2D 类型的可枚举集合。
        var filteredEnemies = enemies.Cast<Node2D>().Where(enemy =>
		{
			return enemy.GlobalPosition.DistanceSquaredTo(player.GlobalPosition) < Math.Pow(MAX_RANGE, 2);
		}).ToList();
        /*
         Cast<Node2D>() 方法将 enemies 数组中的每个元素转换为 Node2D 类型。这是因为我们需要使用 Node2D 类型的方法和属性，例如 GlobalPosition

        Where 是 LINQ 中的一个方法，用于筛选集合中的元素。它接受一个 lambda 表达式作为参数，该表达式定义了筛选条件。

        enemy => { ... } 是一个 lambda 表达式，其中 enemy 是 Cast<Node2D>() 转换后的每个元素。

        enemy.GlobalPosition.DistanceSquaredTo(player.GlobalPosition) 计算敌人节点 enemy 与玩家节点 player 之间的距离的平方。

        Math.Pow(MAX_RANGE, 2) 计算 MAX_RANGE 的平方。

        如果敌人节点 enemy 与玩家节点 player 之间的距离的平方小于 MAX_RANGE 的平方，则该敌人节点满足筛选条件，会被包含在结果中。

        ToList() 方法将筛选后的结果转换为一个列表（List<Node2D>），并存储在 filteredEnemies 变量中。
         */
        if (filteredEnemies.Count == 0)
        {
            GD.Print("No enemies within range.");
            return;
        }
        // 自定义排序
        filteredEnemies.Sort((a, b) =>
		{
            float distanceA = a.GlobalPosition.DistanceSquaredTo(player.GlobalPosition);
            float distanceB = b.GlobalPosition.DistanceSquaredTo(player.GlobalPosition);
            return distanceA.CompareTo(distanceB);
        });

        Node2D sword_instance = sword_ability.Instantiate() as Node2D;
        if (sword_instance == null)
        {
            GD.PrintErr("Failed to instantiate sword_ability.");
            return;
        }
        player.GetParent().AddChild(sword_instance);
		sword_instance.GlobalPosition = filteredEnemies[0].GlobalPosition;
        //给剑一个随机位移
        //random.NextDouble() 是 Random 类的一个方法，它会返回一个在 0.0 和 1.0 之间的随机浮点数。这个值是一个均匀分布的随机数。
        //Rotated 是 Vector2 类的一个方法，用于将向量旋转一个给定的角度（以弧度为单位）。
        sword_instance.GlobalPosition += Vector2.Right.Rotated((float)(2 +(2*Math.PI - 2)*random.NextDouble())) * 4;
        Vector2 enemy_direction = filteredEnemies[0].GlobalPosition - sword_instance.GlobalPosition;
        sword_instance.Rotation = enemy_direction.Angle();
    }
}
