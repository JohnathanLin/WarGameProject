using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ʼ��Ϸ����
/// </summary>
public class StartView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        Find<Button>("startBtn").onClick.AddListener(onStartGameBtn);
        Find<Button>("setBtn").onClick.AddListener(onSetBtn);
        Find<Button>("quitBtn").onClick.AddListener(onQuitGameBtn);
    }

    //��ʼ��Ϸ
    private void onStartGameBtn()
    {
        //�رտ�ʼ����
        GameApp.ViewManager.Close(ViewId);

        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "map";
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }

    //���ð�ť
    private void onSetBtn()
    {
        ApplyFunc(Defines.OpenSetView);
    }

    private void onQuitGameBtn()
    {
        Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenMessageView, new MessageInfo()
        {
            MsgTxt = "���Ƿ�Ҫ�˳���Ϸ��",
            okCallback = delegate ()
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        });
    }
}
