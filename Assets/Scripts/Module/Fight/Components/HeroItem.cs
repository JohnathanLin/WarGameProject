﻿using System.Collections;
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
    }

    public void OnDrag(PointerEventData eventData)
    {


    }
}
