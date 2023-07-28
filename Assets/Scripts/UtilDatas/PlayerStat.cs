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
        hp = 100;
        maxHp = 100;
        attack = 10;
        defence = 5;
        speed = 5.0f;
        exp = 0;
        gold = 100;
    }
}
