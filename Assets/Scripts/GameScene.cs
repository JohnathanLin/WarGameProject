using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//继承mono的游戏脚本，需要挂载游戏物体
public class GameScene : MonoBehaviour
{
    public Texture2D mouseTxt; //鼠标图片
    float dt;

    private static bool isLoaded = false;
    private void Awake()
    {
        if (isLoaded)
        {
            Destroy(gameObject);
        } else
        {
            isLoaded = true;
            DontDestroyOnLoad(gameObject);
            GameApp.Instance.Init();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //设置鼠标样式
        Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);

        //注册配置表
        RegisterConfigs();

        GameApp.ConfigManager.LoadAllConfigs(); //加载配置表

        //测试配置表
        ConfigData enemyConfig = GameApp.ConfigManager.GetConfigData("enemy");
        string name = enemyConfig.getDataById(10001)["Name"];
        Debug.Log(name);

        //播放音乐
        GameApp.SoundManager.PlayBGM("login");

        RegisterModule(); //注册游戏中的控制器
        InitModule();
    }


    //注册控制器
    void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Game, new GameController());
        GameApp.ControllerManager.Register(ControllerType.Loading, new LoadingController());
        GameApp.ControllerManager.Register(ControllerType.Level, new LevelController());
    }

    //执行所有控制器初始化
    void InitModule()
    {
        GameApp.ControllerManager.InitAllModule();
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);   
    }

    void RegisterConfigs()
    {
        GameApp.ConfigManager.Register("enemy", new ConfigData("enemy"));
        GameApp.ConfigManager.Register("level", new ConfigData("level"));
        GameApp.ConfigManager.Register("option", new ConfigData("option"));
        GameApp.ConfigManager.Register("player", new ConfigData("player"));
        GameApp.ConfigManager.Register("role", new ConfigData("role"));
        GameApp.ConfigManager.Register("skill", new ConfigData("skill"));
    }
}
