using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ս�� ��Ҫ������߼�
public class FightEnter : FightUnitBase
{
    public override void Init()
    {
        //��ͼ��ʼ��
        GameApp.MapManager.Init();
    }
}
