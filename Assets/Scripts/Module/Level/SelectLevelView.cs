using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelView : BaseView
{
    protected override void OnStart()
    {
        Find<Button>("close").onClick.AddListener(onCloseBtn);
    }

    private void onCloseBtn()
    {
        GameApp.ViewManager.Close(ViewId);

        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "game";
        loadingModel.callback = delegate ()
        {
            Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }

    public void ShowLevelDes()
    {
        Find("level").SetActive(true);
    }

    public void HideLevelDes()
    {
        Find("level").SetActive(false);
    }
}
