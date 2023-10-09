using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{

    [SerializeField] protected int level;
    [SerializeField] protected int hp;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int attack;
    [SerializeField] protected int defence;
    [SerializeField] protected float speed;

    public int Level { get { return level; } set { level = value; } }
    public int Hp { get { return hp; } set { hp = value; } }
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public int Attack { get { return attack; } set { attack = value; } }
    public int Defence { get { return defence; } set { defence = value; } }
    public float Speed { get { return speed; } set { speed = value; } }

    private void Start()
    {
        level = 1;
        hp = 100;
        maxHp = 100;
        attack = 10;
        defence = 5;
        speed = 10.0f;
    }

    public virtual void OnAttacked(Stat attackerStat)
    {
        int damage = attackerStat.Attack - Defence >= 0 ? attackerStat.Attack - Defence : 1;
        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0;
            OnDead(attackerStat);
        }
    }

    protected virtual void OnDead(Stat attackerStat)
    {
        PlayerStat playerStat = attackerStat as PlayerStat;
        if(playerStat != null)
        {
            playerStat.Exp += 11;
        }
        MasterManager.Game.DeSpawn(gameObject);
    }
}