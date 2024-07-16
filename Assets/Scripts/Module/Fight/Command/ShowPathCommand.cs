using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ʾ�ƶ�·����ָ��
public class ShowPathCommand : BaseCommand
{
    Collider2D pre;//���֮ǰ��⵽��2d��ײ��
    Collider2D current; //��굱ǰ��⵽��2d��ײ��
    AStar astar; //A�Ƕ���
    AStarPoint start; //��ʼ��
    AStarPoint end; //�յ�
    List<AStarPoint> prePathList; //֮ǰ��⵽��·������ ��������� 
    public ShowPathCommand(ModelBase model) : base(model)
    {
        prePathList = new List<AStarPoint>();
        start = new AStarPoint(model.RowIndex, model.ColIndex);
        astar = new AStar(GameApp.MapManager.RowCount, GameApp.MapManager.ColCount);
    }

    public override bool Update(float dt)
    {
        //�������ȷ���ƶ���λ��
        if (Input.GetMouseButtonDown(0))
        {
            if (prePathList.Count > 0 && this.model.Step >= prePathList.Count - 1)
            {
                GameApp.CommandManager.AddCommand(new MoveCommand(this.model, this.prePathList)); //�ƶ�
            }
            else
            {
                GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
            }


            return true;
        }

        current = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition); //��⵱ǰ���λ���Ƿ���2d��ײ��
        if (current != null)
        {
            //֮ǰ�ļ����ײ�к͵�ǰ�ĺ��Ӳ�һ�£��Ž���·�����
            if (current != pre)
            {
                pre = current;

                Block b = current.GetComponent<Block>();
                if (b != null)
                {
                    //��⵽block�ű������� ����Ѱ·
                    end = new AStarPoint(b.RowIndex, b.ColIndex);
                    astar.FindPath(start, end, updatePath);
                } 
                else
                {
                    //û��⵽ ��֮ǰ��·�����
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
        //���֮ǰ�Ѿ���·���� Ҫ�����
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
