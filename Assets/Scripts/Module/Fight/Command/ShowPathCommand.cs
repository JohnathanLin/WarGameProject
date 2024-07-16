using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//显示移动路径的指令
public class ShowPathCommand : BaseCommand
{
    Collider2D pre;//鼠标之前检测到的2d碰撞盒
    Collider2D current; //鼠标当前检测到的2d碰撞盒
    AStar astar; //A星对象
    AStarPoint start; //开始点
    AStarPoint end; //终点
    List<AStarPoint> prePathList; //之前检测到的路径集合 用来清空用 
    public ShowPathCommand(ModelBase model) : base(model)
    {
        prePathList = new List<AStarPoint>();
        start = new AStarPoint(model.RowIndex, model.ColIndex);
        astar = new AStar(GameApp.MapManager.RowCount, GameApp.MapManager.ColCount);
    }

    public override bool Update(float dt)
    {
        //点击鼠标后，确定移动的位置
        if (Input.GetMouseButtonDown(0))
        {
            if (prePathList.Count > 0 && this.model.Step >= prePathList.Count - 1)
            {
                GameApp.CommandManager.AddCommand(new MoveCommand(this.model, this.prePathList)); //移动
            }
            else
            {
                GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
            }


            return true;
        }

        current = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition); //检测当前鼠标位置是否有2d碰撞体
        if (current != null)
        {
            //之前的检测碰撞盒和当前的盒子不一致，才进行路径检测
            if (current != pre)
            {
                pre = current;

                Block b = current.GetComponent<Block>();
                if (b != null)
                {
                    //检测到block脚本的物体 进行寻路
                    end = new AStarPoint(b.RowIndex, b.ColIndex);
                    astar.FindPath(start, end, updatePath);
                } 
                else
                {
                    //没检测到 将之前的路径清除
                    for (int i = 0; i < prePathList.Count; i++)
                    {
                        GameApp.MapManager.mapArr[prePathList[i].RowIndex, prePathList[i].ColIndex].SetDirSp(null, Color.white);
                    }
                    prePathList.Clear();
                }
            }
        }


        return base.Update(dt);
    }

    private void updatePath(List<AStarPoint> pathList)
    {
        //如果之前已经有路径了 要先清除
        if (prePathList.Count > 0)
        {
            for (int i = 0;i < prePathList.Count; i++)
            {
                GameApp.MapManager.mapArr[prePathList[i].RowIndex, prePathList[i].ColIndex].SetDirSp(null, Color.white);
            }
        }

        if (pathList.Count >= 2 && model.Step >= pathList.Count - 1)
        {
            for (int i = 0; i < pathList.Count; i++)
            {
                BlockDirection dir = BlockDirection.down;

                if (i == 0)
                {
                    dir = GameApp.MapManager.GetDirection1(pathList[i], pathList[i + 1]);
                }
                else if (i == pathList.Count - 1)
                {
                    dir = GameApp.MapManager.GetDirection2(pathList[i], pathList[i - 1]);
                }
                else
                {
                    dir = GameApp.MapManager.GetDirection3(pathList[i - 1], pathList[i], pathList[i + 1]);
                }

                GameApp.MapManager.SetBlockDir(pathList[i].RowIndex, pathList[i].ColIndex, dir, Color.yellow);
            }
        }

        Debug.Log("Count: " + pathList.Count);


        prePathList = pathList;
    }
}
