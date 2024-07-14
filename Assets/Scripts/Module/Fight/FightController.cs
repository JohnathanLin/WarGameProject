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

        GameApp.ViewManager.Register(ViewType.DragHeroView, new ViewInfo()
        {
            PrefabName = "DragHeroView",
            controller = this,
            parentTf = GameApp.ViewManager.worldCanvasTf,
            Sorting_Order = 2,
        });

        GameApp.ViewManager.Register(ViewType.TipView, new ViewInfo()
        {
            PrefabName = "TipView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 2,
        });

        GameApp.ViewManager.Register(ViewType.HeroDesView, new ViewInfo()
        {
            PrefabName = "HeroDesView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 2,
        });

        GameApp.ViewManager.Register(ViewType.EnemyDesView, new ViewInfo()
        {
            PrefabName = "EnemyDesView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 2,
        });

        InitModuleEvent();
    }
    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.BeginFight, onBeginFightCallback);
    }

    private void onBeginFightCallback(System.Object []args)
    {
        //Ω¯»Î’Ω∂∑
        GameApp.FightWorldManager.ChangeState(GameState.Enter);

        GameApp.ViewManager.Open(ViewType.FightView);
        GameApp.ViewManager.Open(ViewType.FightSelectHeroView);
    }
}
