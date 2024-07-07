using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//处理拖拽英雄图标的脚本
public class HeroItem : MonoBehaviour
{

    Dictionary<string, string> data;
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("icon").GetComponent<Image>().SetIcon(data["Icon"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;

    }
}
