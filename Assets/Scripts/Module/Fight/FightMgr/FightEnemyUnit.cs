using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˻غ�
/// </summary>
public class FightEnemyUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.FightWorldManager.ResetHeros();
        GameApp.ViewManager.Open(ViewType.TipView, "���˻غ�");

        GameApp.CommandManager.AddCommand(new WaitCommand(1.25f));

        //�����ƶ� ʹ�ü��ܵ�
        for (int i = 0;i < GameApp.FightWorldManager.enemyList.Count;i++)
        {
            Enemy enemy = GameApp.FightWorldManager.enemyList[i];
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f)); //�ȴ�

            GameApp.CommandManager.AddCommand(new AiMoveCommand(enemy)); //�ƶ�

            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f)); //�ȴ�

            GameApp.CommandManager.AddCommand(new SkillCommand(enemy)); //ʹ�ü���

            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f)); //�ȴ�
        }

        //�ȴ�һ��ʱ�� �л�����һغ�
        GameApp.CommandManager.AddCommand(new WaitCommand(0.25f, delegate ()
        {
            GameApp.FightWorldManager.ChangeState(GameState.Player);
        }));
    }
}
