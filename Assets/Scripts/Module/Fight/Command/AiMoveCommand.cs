using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ƶ�ָ��
/// </summary>
public class AiMoveCommand : BaseCommand
{
    Enemy enemy;
    _BFS bfs;
    List<_BFS.Point> pathList;
    _BFS.Point current;
    int pathIndex;
    ModelBase target; //�ƶ�����Ŀ��

    public AiMoveCommand(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy;
        bfs = new _BFS(GameApp.MapManager.RowCount, GameApp.MapManager.ColCount);
        pathList = new List<_BFS.Point>();
    }

    public override void Do()
    {
        base.Do();

        target = GameApp.FightWorldManager.GetMinDisHero(enemy); //��������Ӣ��

        if (target == null)
        {
            //û��Ŀ����
            isFinish = true;
        }
        else
        {
            pathList = bfs.FindMinPath(this.enemy, this.enemy.Step, target.RowIndex, target.ColIndex);
            if (pathList == null)
            {
                //û· ����������һ�������ƶ�
                isFinish = true;
            }
            else
            {
                //����ǰ���˵�λ�����ó�null ���ƶ�
                GameApp.MapManager.ChangeBlockType(this.enemy.RowIndex, this.enemy.ColIndex, BlockType.Null);

            }
        }
    }

    public override bool Update(float dt)
    {
        if (pathList.Count == 0)
        {
            return base.Update(dt);
        }
        else
        {
            current = pathList[pathIndex];
            if (model.Move(current.RowIndex, current.ColIndex, dt * 5) == true) 
            {
                pathIndex++;
                if (pathIndex > pathList.Count - 1)
                {
                    enemy.PlayAni("idle");
                    GameApp.MapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);

                    return true;
                }
            }
        }

        model.PlayAni("move");

        return false;
    }

}
