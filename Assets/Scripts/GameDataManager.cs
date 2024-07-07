using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
    public List<int> heroList;

    public int Money;

    public GameDataManager()
    {
        heroList = new List<int>();

        heroList.Add(10001);
        heroList.Add(10002);
        heroList.Add(10003);

    }
}
