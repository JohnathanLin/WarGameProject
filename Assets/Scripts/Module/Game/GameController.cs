using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ��������������ʼ��Ϸ ���� �˳� �Ȳ�����
/// </summary>
public class GameController : BaseController
{
    public GameController() : base()
    {
        //Ŀǰû����ͼ

        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        //����GameUIController��������¼�
        ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
    }
}
