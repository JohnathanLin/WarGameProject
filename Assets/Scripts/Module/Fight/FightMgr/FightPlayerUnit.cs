using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ҵĻغ�
public class FightPlayerUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.FightWorldManager.ResetEnemies();
        GameApp.ViewManager.Open(ViewType.TipView, "��һغ�");
    }
}
