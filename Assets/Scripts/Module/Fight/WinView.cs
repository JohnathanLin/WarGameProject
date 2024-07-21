using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();

        Find<Button>("bg/okBtn").onClick.AddListener(delegate ()
        {
            //ж��ս���е���Դ
            GameApp.FightWorldManager.ReLoadRes();
            GameApp.ViewManager.CloseAll();

            //�л�����
            LoadingModel load = new LoadingModel();
            load.SceneName = "map";
            load.callback = delegate ()
            {
                GameApp.SoundManager.PlayBGM("mapbgm");
                GameApp.ViewManager.Open(ViewType.SelectLevelView);
            };

            Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, load);
        });
    }
}
