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

    public static MessageCenter MessageCenter;

    public static TimerManager TimerManager;

    public static FightWorldManager FightWorldManager;
    public override void Init()
    {
        SoundManager = new SoundManager();
        ControllerManager = new ControllerManager();
        ViewManager = new ViewManager();
        ConfigManager = new ConfigManager();
        CameraManager = new CameraManager();
        MessageCenter = new MessageCenter();
        TimerManager = new TimerManager();
        FightWorldManager = new FightWorldManager();
    }

    public override void Update(float dt)
    {
        TimerManager.OnUpdate(dt);
        FightWorldManager.Update(dt);
    }
}
