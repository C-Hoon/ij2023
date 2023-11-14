using ClosedXML.Excel;
using GameCore.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class GameData :ScriptableObject
{
    public List<GameCore.Data.LevelUpExp> LevelUpExp;
    public List<GameCore.Data.Item> Items;
    public List<GameCore.Data.Weapon> Weapons;
    public List<GameCore.Data.Boss> Boss;
    public List<GameCore.Data.Monster> Monsters;
}
