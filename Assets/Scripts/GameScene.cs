using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//继承mono的游戏脚本，需要挂载游戏物体
public class GameScene : MonoBehaviour
{
    public Texture2D mouseTxt; //鼠标图片
    float dt;
    private void Awake()
    {
        GameApp.Instance.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        //设置鼠标样式
        Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);
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
}
