using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : BaseController
{
    public LevelController() : base()
    {
        SetModel(new LevelModel());

        //◊¢≤· ”Õº
        GameApp.ViewManager.Register(ViewType.SelectLevelView, new ViewInfo()
        {
            PrefabName = "SelectLevelView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });

        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        model.Init();
    }

    public override void InitGlobalEvent()
    {
        GameApp.MessageCenter.AddEvent(Defines.ShowLevelDesEvent, onShowSelectLevelDes);
        GameApp.MessageCenter.AddEvent(Defines.HideLevelDesEvent, onHideSelectLevelDes);
    }

    public override void RemoveGlobalEvent()
    {
      
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenSelectLevelView, onOpenSelectLevelView);
    }

    private void onShowSelectLevelDes(System.Object arg)
    {
        Debug.Log((int)arg);
        LevelModel levelModel = GetModel<LevelModel>();
        levelModel.current = levelModel.GetLevel((int)arg);

        GameApp.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).ShowLevelDes();
    }

    private void onHideSelectLevelDes(System.Object arg)
    {
        GameApp.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).HideLevelDes();
    }
         
    private void onOpenSelectLevelView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.SelectLevelView);
    }
}
