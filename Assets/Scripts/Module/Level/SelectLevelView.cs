using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelView : BaseView
{
    protected override void OnStart()
    {
        Find<Button>("close").onClick.AddListener(onCloseBtn);
        Find<Button>("level/fightBtn").onClick.AddListener(onFightBtn);
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
        LevelData levelData = Controller.GetModel<LevelModel>().current;
        Find<Text>("level/name/txt").text = levelData.Name;
        Find<Text>("level/des/txt").text = levelData.Des;
    }

    public void HideLevelDes()
    {
        Find("level").SetActive(false);
    }

    private void onFightBtn()
    {
        GameApp.ViewManager.Close(ViewId);

        GameApp.CameraManager.ResetPos();

        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = Controller.GetModel<LevelModel>().current.SceneName;
        loadingModel.callback = delegate ()
        {
            Controller.ApplyControllerFunc(ControllerType.Fight, Defines.BeginFight);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }
}
