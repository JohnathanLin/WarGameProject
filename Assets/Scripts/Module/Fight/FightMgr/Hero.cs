using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ӣ�۽ű�
public class Hero : ModelBase, ISkill
{
    public SkillProperty skillPro { get; set; }

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

        skillPro = new SkillProperty(int.Parse(this.data["Skill"]));
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

            //ִ��δѡ��
            GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);

            if (IsStop == false) 
            {
                //��ʾ·��
                GameApp.MapManager.ShowStepGrid(this, Step);

                //�����ʾ·��ָ��
                GameApp.CommandManager.AddCommand(new ShowPathCommand(this));

                //���ѡ���¼�
                addOptionEvents();
            }

            GameApp.ViewManager.Open(ViewType.HeroDesView, this);
        }
    }

    private void addOptionEvents()
    {
        GameApp.MessageCenter.AddTmpEvent(Defines.OnAttackEvent, onAttackCallBack);
        GameApp.MessageCenter.AddTmpEvent(Defines.OnIdleEvent, onIdleCallBack);
        GameApp.MessageCenter.AddTmpEvent(Defines.OnCancelEvent, onCancelCallBack);
    }

    private void onAttackCallBack(System.Object arg)
    {
        Debug.Log("attack");
    }

    private void onIdleCallBack(System.Object arg)
    {
        IsStop = true;
        Debug.Log("idle");
    }

    private void onCancelCallBack(System.Object arg)
    {
        GameApp.CommandManager.UnDo();
    }

    //δѡ��
    protected override void OnUnSelectCallBack(System.Object args)
    {
        base.OnUnSelectCallBack(args);
        GameApp.ViewManager.Close(ViewType.HeroDesView);
    }
}
