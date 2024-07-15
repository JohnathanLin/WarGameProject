using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ӣ�۽ű�
public class Hero : ModelBase
{
    public void Init(Dictionary<string, string> data, int row, int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(this.data["Id"]);
        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
    }

    //ѡ��
    protected override void OnSelectCallBack(System.Object args)
    {
        //��һغ� ����ѡ�н�ɫ
        if (GameApp.FightWorldManager.state == GameState.Player)
        {
            //���ܲ���
            if (IsStop) 
            {
                return;
            }

            if (GameApp.CommandManager.IsRunningCommand)
            {
                return;
            }

            //�����ʾ·��ָ��
            GameApp.CommandManager.AddCommand(new ShowPathCommand(this));

            base.OnSelectCallBack(args);
            GameApp.ViewManager.Open(ViewType.HeroDesView, this);
        }

    }

    //δѡ��
    protected override void OnUnSelectCallBack(System.Object args)
    {
        base.OnUnSelectCallBack(args);
        GameApp.ViewManager.Close(ViewType.HeroDesView);
    }
}
