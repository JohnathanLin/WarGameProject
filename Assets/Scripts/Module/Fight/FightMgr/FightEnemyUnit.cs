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

        //�ȴ�һ��ʱ�� �л�����һغ�
        GameApp.CommandManager.AddCommand(new WaitCommand(0.25f, delegate ()
        {
            GameApp.FightWorldManager.ChangeState(GameState.Player);
        }));
    }
}
