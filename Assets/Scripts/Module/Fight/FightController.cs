using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : BaseController
{
    public FightController() : base()
    {
        GameApp.ViewManager.Register(ViewType.FightView, new ViewInfo()
        {
            PrefabName = "FightView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });

        GameApp.ViewManager.Register(ViewType.FightSelectHeroView, new ViewInfo() 
        {
            PrefabName = "FightSelectHeroView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 1,
        });

        InitModuleEvent();
    }
    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.BeginFight, onBeginFightCallback);
    }

    private void onBeginFightCallback(System.Object []args)
    {
        GameApp.ViewManager.Open(ViewType.FightView);
        GameApp.ViewManager.Open(ViewType.FightSelectHeroView);
    }
}
