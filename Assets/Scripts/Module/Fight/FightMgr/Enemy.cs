using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ModelBase
{
    protected override void Start()
    {
        base.Start();

        data = GameApp.ConfigManager.GetConfigData("enemy").GetDataById(Id);

        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
    }

    //选中
    protected override void OnSelectCallBack(System.Object args)
    {
        if (GameApp.CommandManager.IsRunningCommand)
        {
            return;
        }

        base.OnSelectCallBack(args);

        GameApp.ViewManager.Open(ViewType.EnemyDesView, this);
    }

    //未选中
    protected override void OnUnSelectCallBack(System.Object args)
    {
        base.OnUnSelectCallBack(args);


        GameApp.ViewManager.Close(ViewType.EnemyDesView);
    }
}
