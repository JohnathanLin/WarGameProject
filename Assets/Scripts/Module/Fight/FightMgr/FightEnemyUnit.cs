using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人回合
/// </summary>
public class FightEnemyUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.FightWorldManager.ResetHeros();
        GameApp.ViewManager.Open(ViewType.TipView, "敌人回合");

        GameApp.CommandManager.AddCommand(new WaitCommand(1.25f));

        //敌人移动 使用技能等

        //等待一段时间 切换回玩家回合
        GameApp.CommandManager.AddCommand(new WaitCommand(0.25f, delegate ()
        {
            GameApp.FightWorldManager.ChangeState(GameState.Player);
        }));
    }
}
