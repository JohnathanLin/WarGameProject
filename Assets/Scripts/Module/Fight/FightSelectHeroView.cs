using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightSelectHeroView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("bottom/startBtn").onClick.AddListener(onFightBtn); 
    }

    //选完英雄开始进入玩家回合
    private void onFightBtn()
    {
        //如果一个英雄都没选 要提示玩家 选择
        if (GameApp.FightWorldManager.heroList.Count == 0)
        {
            Debug.Log("没有选择英雄");
        } else
        {
            GameApp.ViewManager.Close(ViewId);

            //切换到玩家回合
            GameApp.FightWorldManager.ChangeState(GameState.Player);
        }
    }
    public override void Open(params object[] args)
    {
        base.Open(args);

        GameObject prefabObj = Find("bottom/grid/item");

        Transform gridTf = Find("bottom/grid").transform;

        for (int i = 0; i < GameApp.GameDataManager.heroList.Count; i++)
        {
            Dictionary<string, string> data = GameApp.ConfigManager.GetConfigData("player").GetDataById(GameApp.GameDataManager.heroList[i]);

            GameObject obj = Object.Instantiate(prefabObj, gridTf);
            
            obj.SetActive(true);

            HeroItem item = obj.AddComponent<HeroItem>();
            item.Init(data);
        }
    }
}
