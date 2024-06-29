using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ConfigManager 
{
    Dictionary<string, ConfigData> loadMap;
    Dictionary<string, ConfigData> configs;

    public ConfigManager() 
    {
        loadMap = new Dictionary<string, ConfigData>();
        configs = new Dictionary<string, ConfigData>();
    }

    public void Register(string fileName, ConfigData data)
    {
        loadMap[fileName] = data;
    }

    public void LoadAllConfigs()
    {
        foreach (var item in loadMap)
        {
            TextAsset textAsset = item.Value.LoadFile();
            item.Value.Load(textAsset.text);
            configs[item.Value.fileName] = item.Value;
        }
        loadMap.Clear();
    }

    public ConfigData GetConfigData(string fileName)
    {
        return configs[fileName];
    }
}
