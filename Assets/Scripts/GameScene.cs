using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//�̳�mono����Ϸ�ű�����Ҫ������Ϸ����
public class GameScene : MonoBehaviour
{
    public Texture2D mouseTxt; //���ͼƬ
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
        //���������ʽ
        Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);

        //ע�����ñ�
        RegisterConfigs();

        GameApp.ConfigManager.LoadAllConfigs(); //�������ñ�

        //�������ñ�
        ConfigData enemyConfig = GameApp.ConfigManager.GetConfigData("enemy");
        string name = enemyConfig.getDataById(10001)["Name"];
        Debug.Log(name);

        //��������
        GameApp.SoundManager.PlayBGM("login");

        RegisterModule(); //ע����Ϸ�еĿ�����
        InitModule();
    }


    //ע�������
    void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Game, new GameController());
        GameApp.ControllerManager.Register(ControllerType.Loading, new LoadingController());
        GameApp.ControllerManager.Register(ControllerType.Level, new LevelController());
    }

    //ִ�����п�������ʼ��
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
