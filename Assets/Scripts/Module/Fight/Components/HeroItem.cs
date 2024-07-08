﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//处理拖拽英雄图标的脚本
public class HeroItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Dictionary<string, string> data;

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("icon").GetComponent<Image>().SetIcon(data["Icon"]);
    }

    public void Init(Dictionary<string, string> data)

    {
        this.data = data;

    }

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Open(ViewType.DragHeroView, data["Icon"]);
    }

    //结束拖拽
    public void OnEndDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Close((int)ViewType.DragHeroView);
        //检测拖拽后的位置是否有block脚本
        Tools.ScreenPointToRay2D(eventData.pressEventCamera, eventData.position, delegate(Collider2D col)
        {
            if (col != null)
            {
                Block b = col.GetComponent<Block>();
                if (b != null)
                {
                    //有方块
                    Debug.Log(b);
                    Destroy(gameObject);//删除
                    //创建英雄物体
                    GameApp.FightWorldManager.AddHero(b, data);
                }
            }
        });
    }

    public void OnDrag(PointerEventData eventData)
    {


    }
}
