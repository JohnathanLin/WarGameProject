using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//英雄脚本
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

    //选中
    protected override void OnSelectCallBack(System.Object args)
    {
        //玩家回合 才能选中角色
        if (GameApp.FightWorldManager.state == GameState.Player)
        {
            //不能操作
            if (IsStop) 
            {
                return;
            }

            if (GameApp.CommandManager.IsRunningCommand)
            {
                return;
            }

            //执行未选中
            GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);

            if (IsStop == false) 
            {
                //显示路径
                GameApp.MapManager.ShowStepGrid(this, Step);

                //添加显示路径指令
                GameApp.CommandManager.AddCommand(new ShowPathCommand(this));

                //添加选项事件
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

    //未选中
    protected override void OnUnSelectCallBack(System.Object args)
    {
        base.OnUnSelectCallBack(args);
        GameApp.ViewManager.Close(ViewType.HeroDesView);
    }
}
