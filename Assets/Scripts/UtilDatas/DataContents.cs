using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Temp
public class Temp
{
    public int characterID;
    public float attackPower;
    public string characterName;
}

public class TempData : ILoader<int, Temp>
{
    public List<Temp> tempList = new List<Temp>();
    public Dictionary<int, Temp> MakeDict() 
    {
        Dictionary<int, Temp> dict = new Dictionary<int, Temp>();
        foreach (Temp temp in tempList)
        {
            dict.Add(temp.characterID, temp);
        }
        return dict;
    }
}
#endregion
