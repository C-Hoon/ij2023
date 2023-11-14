using GameCore.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelecter : MonoBehaviour
{
    [SerializeField]
    List<WeaponInfo> weapons;
    Weapon selectedWeapon;
    public Button startButton;

    private void Start()
    {
        AllDeSelected();
        GameCore.Managers.Resource.Instantiate("Character/Player");
    }
    public void SetSelectedItem(WeaponInfo weapon)
    {
        selectedWeapon = weapon.weaponScriptable;
        startButton.interactable = true;
    }
    public void AllDeSelected()
    {
        foreach (var weapon in weapons)
        {
            weapon.DeSelected();
        }
        startButton.interactable = false;
    }
    public void SetPlayerEquipment()
    {
        PlayerStat stat = GameCore.Managers.Game.Player.GetComponent<PlayerMove>().playerStat;
        stat.AddToWeapon(selectedWeapon);
        stat.itemDictionary = new Dictionary<Item, int>();

        Debug.Log(stat.weapon.equipmentName);
        PlayerPrefs.SetString(typeof(Weapon).Name, stat.weapon.equipmentName);
    }
}
