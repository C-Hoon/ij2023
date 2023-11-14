using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public int level;
    public int hp;
    public int mp;
    public int exp;
    public int attack;
    public int defense;
    public float moveSpeed;
    public float jumpPower;
    public Stat(int level = 1, int hp = 0, int mp = 0, int exp = 0, int attack = 0, int defense = 0, int moveSpeed = 0, int jumpPower = 0)
    {
        this.level = level;
        this.hp = hp;
        this.mp = mp;
        this.exp = exp;
        this.attack = attack;
        this.defense = defense;
        this.moveSpeed = moveSpeed;
        this.jumpPower = jumpPower;
    }
    public Stat(Stat other)
    {
        this.level = other.level;
        this.hp = other.hp;
        this.mp = other.mp;
        this.exp = other.exp;
        this.attack = other.attack;
        this.defense = other.defense;
        this.moveSpeed = other.moveSpeed;
        this.jumpPower = other.jumpPower;
    }
    public virtual void Init(int level = 1)
    {
        if (level < 1)
            level = 1;
        level--;
        GameCore.Data.LevelUpExp LevelUpExpList = GameCore.Managers.Data.gameData.LevelUpExp[1];
        Stat stat = LevelUpExpList.GetStat();
        this.level = stat.level;
        this.hp = stat.hp;
        this.mp = stat.mp;
        this.exp = stat.exp;
        this.attack = stat.attack;
        this.defense = stat.defense;
        this.moveSpeed = stat.moveSpeed;
        this.jumpPower = stat.jumpPower;
    }
    public void Add(Stat other)
    {
        this.hp += other.hp;
        this.mp += other.mp;
        this.exp += other.exp;
        this.attack += other.attack;
        this.defense += other.defense;
        this.moveSpeed += other.moveSpeed;
        this.jumpPower += other.jumpPower;
    }
}
