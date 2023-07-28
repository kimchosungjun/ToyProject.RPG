using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager 
{
    public Dictionary<int, Data.Stat> statDict { get; private set; } = new Dictionary<int, Data.Stat>();

    public void Init()
    {
        statDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = MasterManager.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
