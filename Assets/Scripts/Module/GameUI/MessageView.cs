using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageInfo
{
    public string MsgTxt;
    public System.Action okCallback;
    public System.Action noCallBack;
}
public class MessageView : BaseView
{
    MessageInfo info;

    protected override void OnAwake()
    {
        base.OnAwake();

        Find<Button>("okBtn").onClick.AddListener(OnOkBtn);
        Find<Button>("noBtn").onClick.AddListener(OnNoBtn);
    

    }

    private void OnOkBtn()
    {
        info.okCallback?.Invoke();
    }

    private void OnNoBtn()
    {
        info.noCallBack?.Invoke();
        GameApp.ViewManager.Close(ViewId);
    }

    public override void Open(params object[] args)
    {
        info = args[0] as MessageInfo;
        Find<Text>("content/txt").text = info.MsgTxt;
    }
}
