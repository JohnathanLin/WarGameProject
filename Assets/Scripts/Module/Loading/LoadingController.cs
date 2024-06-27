using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : BaseController
{
    AsyncOperation asyncOp;

    public LoadingController():base() 
    {
        //注册视图

        GameApp.ViewManager.Register(ViewType.LoadingView, new ViewInfo()
        {
            PrefabName = "LoadingView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });

        InitModuleEvent();
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.LoadingScene, loadSceneCallback);
    }

    private void loadSceneCallback(System.Object[] arg)
    {
        LoadingModel loadingModel = arg[0] as LoadingModel;

        SetModel(loadingModel);

        //打开加载界面
        GameApp.ViewManager.Open(ViewType.LoadingView);

        //加载场景
        asyncOp = SceneManager.LoadSceneAsync(loadingModel.SceneName);

        asyncOp.completed += onLoadedEndCallBack;
    }

    private void onLoadedEndCallBack(AsyncOperation asyncOp)
    {
        asyncOp.completed -= onLoadedEndCallBack;

        GetModel<LoadingModel>().callback?.Invoke();

        GameApp.ViewManager.Close((int)ViewType.LoadingView);
    }
}
