using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField] int exp;
    [SerializeField] int gold;
    public int Exp { get { return exp; } set { exp = value; } }
    public int Gold { get { return gold; } set { gold = value; } }

    private void Start()
    {
        level = 1;
        SetStat(level);
        attack = 100;
        maxHp = 100;
        hp = 100;
        exp = 0;
        defence = 5;
        speed = 5;
        gold = 0;
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = MasterManager.Data.statDict;
        Debug.Log(dict.Count);
        //Data.Stat stat = dict[level];
        //hp = stat.maxHp;
        //maxHp = stat.maxHp;
        //attack = stat.attack;
    }

    protected override void OnDead(Stat attackerStat)
    {
        Debug.Log("플레이어 죽음!");
    }

    public void CheckExp()
    {
        int level = Level;
        while (true)
        {
            if (MasterManager.Data.statDict.TryGetValue(level + 1, out Data.Stat stat) == false)
                break;
            if (exp < stat.totalExp)
                break;
            level++;
        }
        if (level != Level)
        {
            Level = level; 
            SetStat(level);
        }
    }
}
