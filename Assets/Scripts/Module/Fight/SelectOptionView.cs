using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOptionView : BaseView
{
    Dictionary<int, OptionItem> opItemMap;

    public override void InitData()
    {
        base.InitData();
        opItemMap = new Dictionary<int, OptionItem>();
        FightModel fightModel = Controller.GetModel() as FightModel;

        List<OptionData> opList = fightModel.optionList;

        Transform tf = Find("bg/grid").transform;
        GameObject prefabObj = Find("bg/grid/item");

        for (int i = 0; i < opList.Count; i++)
        {
            GameObject obj = Object.Instantiate(prefabObj, tf);
            obj.SetActive(true);

            OptionItem item = obj.AddComponent<OptionItem>();
            item.Init(opList[i]);

            opItemMap.Add(opList[i].Id, item);
        }
    }

    public override void Open(params object[] args)
    {
        //传入两个参数 一个是英雄的Event字符串 对应 的选项Id 需要切割
        //第二个参数 是 鼠标位置
        //Event 1001-1002-1005

        string[] evtArr = args[0].ToString().Split("-");
        Find("bg/grid").transform.position = (Vector2)args[1];

        foreach (var item in opItemMap)
        {
            item.Value.gameObject.SetActive(false);
        }

        for (int i = 0; i < evtArr.Length; i++)
        {
            opItemMap[int.Parse(evtArr[i])].gameObject.SetActive(true);
        }

    }
}
