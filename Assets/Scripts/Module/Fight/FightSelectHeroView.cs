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

    //ѡ��Ӣ�ۿ�ʼ������һغ�
    private void onFightBtn()
    {
        //���һ��Ӣ�۶�ûѡ Ҫ��ʾ��� ѡ��
        if (GameApp.FightWorldManager.heroList.Count == 0)
        {
            Debug.Log("û��ѡ��Ӣ��");
        } else
        {
            GameApp.ViewManager.Close(ViewId);

            //�л�����һغ�
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
