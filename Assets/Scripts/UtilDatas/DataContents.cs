using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Data
{
    #region Stat
    [Serializable]
    public class Stat
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> statList = new List<Stat>();
        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in statList)
            {
                dict.Add(stat.level, stat);
            }
            return dict;
        }
    }
    #endregion
}

[Serializable]
public class ItemInfo
{
    public int ID;
    public string Name;
    public string Description;
}

public class ItemData : ILoader<int, ItemInfo>
{
    public List<ItemInfo> Items = new List<ItemInfo>();
    public Dictionary<int,ItemInfo> MakeDict()
    {
        Dictionary<int, ItemInfo> dict = new Dictionary<int, ItemInfo>();
        foreach (ItemInfo itemInfo in Items)
        {
            dict.Add(itemInfo.ID, itemInfo);
        }
        return dict;
    }
}

