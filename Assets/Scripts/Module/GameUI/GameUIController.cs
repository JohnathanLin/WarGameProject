using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����һЩ��Ϸͨ��ui�Ŀ�����(������� ��ʾ��� ��Ϸ��ʼ���������������ע��)
/// </summary>
public class GameUIController : BaseController
{
    public GameUIController() : base()
    {
        //ע����ͼ

        //��ʼ��ͼ
        GameApp.ViewManager.Register(ViewType.StartView, new ViewInfo()
        {
            PrefabName = "StartView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });

        //������ͼ
        GameApp.ViewManager.Register(ViewType.SetView, new ViewInfo()
        {
            PrefabName = "SetView",
            controller = this,
            Sorting_Order = 1,
            parentTf = GameApp.ViewManager.canvasTf
        });

        InitModuleEvent(); //��ʼ��ģ���¼�
        InitGlobalEvent(); //��ʼ��ȫ���¼�
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenStartView, openStartView); //ע��򿪿�ʼ���
        RegisterFunc(Defines.OpenSetView, openSetView); //ע��򿪿�ʼ���
    }

    //����ģ��ע���¼� ����
    private void openStartView(System.Object[] arg)
    {
        GameApp.ViewManager.Open(ViewType.StartView, arg);
    }

     //����ģ��ע���¼� ����
    private void openSetView(System.Object[] arg)
    {
        GameApp.ViewManager.Open(ViewType.SetView, arg);
    }
}
