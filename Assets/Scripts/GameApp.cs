using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 统一定义游戏中的管理器，在此类进行初始化
/// </summary>
public class GameApp:Singleton<GameApp>
{
    public static SoundManager SoundManager; //音频管理器定义

    public static ControllerManager ControllerManager; //控制器管理器定义

    public static ViewManager ViewManager; //视图管理器定义

    public static ConfigManager ConfigManager; //配置表管理器定义

    public static CameraManager CameraManager;

    public static MessageCenter MessageCenter;

    public static TimerManager TimerManager;

    public static FightWorldManager FightWorldManager;

    public static MapManager MapManager;

    public static GameDataManager GameDataManager;

    public static UserInputManager UserInputManager;

    public static CommandManager CommandManager;

    public static SkillManager SkillManager;
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
        MapManager = new MapManager();
        GameDataManager = new GameDataManager();
        UserInputManager = new UserInputManager();
        CommandManager = new CommandManager();
        SkillManager = new SkillManager();
    }

    public override void Update(float dt)
    {
        TimerManager.OnUpdate(dt);
        FightWorldManager.Update(dt);
        UserInputManager.Update();
        CommandManager.Update(dt);
        SkillManager.Update(dt);
    }
}
