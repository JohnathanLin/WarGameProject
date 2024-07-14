using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigData
{
    private Dictionary<int, Dictionary<string, string>> datas;
    public string fileName;
    public ConfigData(string fileName) 
    {
        this.fileName = fileName;
        datas = new Dictionary<int, Dictionary<string, string>>();
    }

    public TextAsset LoadFile()
    {
        return Resources.Load<TextAsset>($"Data/{fileName}");
    }

    public void Load(string txt)
    {
        string[] dataArr = txt.Split("\r\n");
        string[] title = dataArr[0].Split(",");
       
        for (int i = 2;i < dataArr.Length;i++)
        {
            Dictionary<string, string> dataMap = new Dictionary<string, string>();
            string[] valueArr = dataArr[i].Split(",");
            for (int j = 0;j < valueArr.Length;j++)
            {
                dataMap[title[j]] = valueArr[j];
            }
            datas.Add(int.Parse(dataMap["Id"]), dataMap);
        }
      
    }

    public Dictionary<string, string> GetDataById(int id)
    {
        return datas[id];
    }

    public Dictionary<int, Dictionary<string, string>> getLines()
    {
        return datas;
    }
}
