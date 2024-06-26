using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始游戏界面
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

    //开始游戏
    private void onStartGameBtn()
    {

    }

    //设置按钮
    private void onSetBtn()
    {
        ApplyFunc(Defines.OpenSetView);
    }

    private void onQuitGameBtn()
    {

    }
}
