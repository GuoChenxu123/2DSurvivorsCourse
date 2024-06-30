using Godot;
using System;

public partial class game_camera : Camera2D
{
    //这是一个不受绘制顺序影响的节点


    Vector2 target_position = Vector2.Zero;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //强制此Camera2D成为当前活动的Camera2D。
        MakeCurrent();//MakeCurrent() 方法用于将当前的 Camera2D 设置为活动相机。
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        AcquireTarget();
        //lerp是线性插值，将当前位置与目标位置之间进行插值，得到一个平滑过渡的效果。
        //线性插值的实现，用于平滑地将相机的当前位置过渡到目标位置。
        GlobalPosition = GlobalPosition.Lerp(target_position, (float)(1.0f - Math.Exp(-delta * 10)));
        /*
             这个公式用于计算插值的权重，使得相机的移动更加平滑。
            Math.Exp(-delta * 10) 计算的是一个指数衰减函数，其中 delta 是帧时间间隔，10 是一个调整参数。
            1.0f - Math.Exp(-delta * 10) 的结果是一个介于 0 和 1 之间的值，随着时间的推移逐渐接近 1。
            这个值作为插值的权重，使得相机的移动在开始时较慢，然后逐渐加速，最终接近目标位置。
         */
    }

    public void AcquireTarget()
    {
        Godot.Collections.Array<Node> player_nodes = GetTree().GetNodesInGroup("player");
        if (player_nodes.Count > 0)
        {
            Node2D player = player_nodes[0] as Node2D;
            target_position = player.GlobalPosition;
        }
    }
}
