using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ҵĻغ�
public class FightPlayerUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        Debug.Log("��һغ�");
        GameApp.ViewManager.Open(ViewType.TipView, "��һغ�");
    }
}
