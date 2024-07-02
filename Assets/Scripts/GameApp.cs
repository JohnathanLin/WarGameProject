using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ͳһ������Ϸ�еĹ��������ڴ�����г�ʼ��
/// </summary>
public class GameApp:Singleton<GameApp>
{
    public static SoundManager SoundManager; //��Ƶ����������

    public static ControllerManager ControllerManager; //����������������

    public static ViewManager ViewManager; //��ͼ����������

    public static ConfigManager ConfigManager; //���ñ����������

    public static CameraManager CameraManager;
    public override void Init()
    {
        SoundManager = new SoundManager();
        ControllerManager = new ControllerManager();
        ViewManager = new ViewManager();
        ConfigManager = new ConfigManager();
        CameraManager = new CameraManager();
    }
}
