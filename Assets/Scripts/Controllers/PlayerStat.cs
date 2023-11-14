using GameCore.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerStat : Stat
{
    public int critRate;
    public int critDamage;

    public Dictionary<Item, int> itemDictionary;
    public Weapon weapon;

    public PlayerStat(int level = 1, int hp = 0, int mp = 0, int exp = 0, int attack = 0, int defense = 0, int moveSpeed = 0, int jumpPower = 0, int critRate = 0, int critDamage = 0) : base(level, hp, mp, exp, attack, defense, moveSpeed, jumpPower)
    {
        this.critRate = critRate;
        this.critDamage = critDamage;
        itemDictionary = new Dictionary<Item, int>();
        weapon = new Weapon();
        Debug.Log("PlayerStat»ý¼ºÀÚ");
    }
    public PlayerStat(PlayerStat other) : base(other)
    {
        this.critRate = other.critRate;
        this.critDamage = other.critDamage;
        itemDictionary = new Dictionary<Item, int>();
        weapon = new Weapon();
    }
    public override void Init(int level = 1)
    {
        base.Init(level);
        this.critRate = 0;
        this.critDamage = 50;
    }
    public void LevelUp()
    {
        if (level > 15)
        {
            level = 15;
            return;
        }
        level++;
        base.Init(level);

        foreach (var item in itemDictionary)
        {
            int i = 0;
            while (i++ < item.Value)
            {
                item.Key.SetUpItem();
            }
        }
    }

    public void Add(PlayerStat other)
    {
        base.Add(other);
        this.critRate += other.critRate;
        this.critDamage += other.critDamage;
    }

    public void AddToItemList(Item item)
    {
        if (!itemDictionary.TryAdd(item, 1))
            itemDictionary[item]++;
        item.SetUpItem();
        Debug.Log($"{critRate}, {critDamage}");
    }
    public void AddToWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void ClearItemList()
    {
        foreach(var item in itemDictionary)
        {
            item.Key.TakeDownItem();
        }
        itemDictionary.Clear();
    }
    public void ClearWeapon()
    {
        weapon = null;
    }

    public void PlayerPrefsClear()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Save()
    {
        GameCore.Managers.Game.SaveStage();
        PlayerPrefsClear();
        SaveStat();
        SaveItemDictionary();
        SaveWeapon();
    }
    void SaveStat()
    {
        PlayerPrefs.SetInt(level.GetType().Name, level);
        PlayerPrefs.SetInt(hp.GetType().Name, hp);
        PlayerPrefs.SetInt(mp.GetType().Name, mp);
        PlayerPrefs.SetInt(exp.GetType().Name, exp);
        PlayerPrefs.SetInt(attack.GetType().Name, attack);
        PlayerPrefs.SetInt(defense.GetType().Name, defense);
        PlayerPrefs.SetFloat(moveSpeed.GetType().Name, moveSpeed);
        PlayerPrefs.SetFloat(jumpPower.GetType().Name, jumpPower);
        PlayerPrefs.SetInt(critRate.GetType().Name, critRate);
        PlayerPrefs.SetInt(critDamage.GetType().Name, critDamage);
    }
    void SaveItemDictionary()
    {
        foreach (var item in itemDictionary)
        {
            PlayerPrefs.SetInt(item.Key.equipmentName, item.Value);
        }
        //weapon;

        PlayerPrefs.Save();
    }
    void SaveWeapon()
    {
        PlayerPrefs.SetString(typeof(Weapon).Name, weapon.equipmentName);
    }


    public void Load()
    {
        GameCore.Managers.Game.LoadStage();
        LoadStat();
        LoadItemDictionary();
        LoadWeapon();
    }
    void LoadStat()
    {
        level = PlayerPrefs.GetInt(level.GetType().Name);
        hp = PlayerPrefs.GetInt(hp.GetType().Name);
        mp = PlayerPrefs.GetInt(mp.GetType().Name);
        exp = PlayerPrefs.GetInt(exp.GetType().Name);
        attack = PlayerPrefs.GetInt(attack.GetType().Name);
        defense = PlayerPrefs.GetInt(defense.GetType().Name);
        moveSpeed = PlayerPrefs.GetInt(moveSpeed.GetType().Name);
        jumpPower = PlayerPrefs.GetInt(jumpPower.GetType().Name);
        critRate = PlayerPrefs.GetInt(critRate.GetType().Name);
        critDamage = PlayerPrefs.GetInt(critDamage.GetType().Name);
    }
    void LoadWeapon()
    {
        string weaponName = PlayerPrefs.GetString(typeof(Weapon).Name);
        if(string.IsNullOrEmpty(weaponName))
            return;
        Weapon weaponScriptableObject = Resources.Load<Weapon>("Equipment/Weapons/"+ weaponName);
        AddToWeapon(weaponScriptableObject);
    }
    void LoadItemDictionary()
    {
        if(itemDictionary != null)
            itemDictionary.Clear();
        itemDictionary = new Dictionary<Item, int>();
        List<Item> items = GameCore.Managers.Data.LoadScriptableObjects<Item>("Equipment/Items");

        foreach(var item in items)
        {
            if (PlayerPrefs.HasKey(item.equipmentName))
            {
                itemDictionary.Add(item, PlayerPrefs.GetInt(item.equipmentName));
            }
        }
    }
}
