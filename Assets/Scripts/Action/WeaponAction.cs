using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponActionType
{
    HealAction,
    ReflectAction,
    ImpactAction,
    BuffStackAction,
}
public class WeaponEvent
{
    public object context;
}


public abstract class WeaponAction
{
    //조건
    public abstract void Run(object context);
}
public class HealAction : WeaponAction
{
    public int healAmount;

    public override void Run(object context)
    {
        //PlayerMove playerMove = context as PlayerMove;
        if(context is PlayerMove playerMove)
        {
            //PlayerMove move = (PlayerMove)context;
            playerMove.ChangeHealth(PlayerMove.ChangeHealthType.UP);
        }
    }
}
public class ReflectAction : WeaponAction
{
    public override void Run(object context)
    {
        //반사
        if (context is EnemyMove enemyMove)
        {
            int damage = GameCore.Managers.Game.Player.GetComponent<PlayerMove>().playerStat.attack / 10;
            enemyMove.OnDamaged(damage);
        }
    }
}
public class ImpactAction : WeaponAction
{
    public override void Run(object context)
    {
        //충격
        if (context is EnemyMove enemyMove)
        {
            int damage = GameCore.Managers.Game.Player.GetComponent<PlayerMove>().playerStat.attack / 20;
            enemyMove.OnDamaged(damage);
            //범위 공격
            //enemyMove.OnDamaged(damage);
        }
    }
}
public class BuffStackAction : WeaponAction
{
    public override void Run(object context)
    {
        //스택
        if (context is EnemyMove enemyMove)
        {
            enemyMove.OnDamaged(1);
        }
    }
}



public class WeaponEventFactory
{
    public static WeaponAction CreateAction(WeaponActionType actionType)
    {
        switch (actionType)
        {
            case WeaponActionType.HealAction:
                return new HealAction();
            case WeaponActionType.ReflectAction:
                return new ReflectAction();
            case WeaponActionType.ImpactAction:
                return new ImpactAction();
            case WeaponActionType.BuffStackAction:
                return new BuffStackAction();
            default:
                throw new ArgumentException("Invalid action type: " + actionType);
        }
    }
}