using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSelectHeroView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);

        GameObject prefabObj = Find("bottom/grid/item");

        Transform gridTf = Find("bottom/grid").transform;

        for (int i = 0; i < GameApp.GameDataManager.heroList.Count; i++)
        {
            Dictionary<string, string> data = GameApp.ConfigManager.GetConfigData("player").getDataById(GameApp.GameDataManager.heroList[i]);

            GameObject obj = Object.Instantiate(prefabObj, gridTf);
            
            obj.SetActive(true);

            HeroItem item = obj.AddComponent<HeroItem>();
            item.Init(data);
        }
    }
}
