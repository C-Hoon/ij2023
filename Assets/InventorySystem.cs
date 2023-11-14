using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    PlayerStat stat { get { return GameCore.Managers.Game.Player.GetComponent<PlayerMove>().playerStat; } }
    public TextMeshProUGUI infoText;

    [SerializeField]
    Image weapon;

    [SerializeField]
    GameObject itemTableUI;

    [SerializeField]
    GameObject inventoryItemPrefab;
    private void Start()
    {
        Init();
    }
    void Init()
    {
        infoText.text =
            $"hp : {stat.hp} \n" +
            $"mp : {stat.mp} \n" +
            $"attack : {stat.attack} \n" +
            $"defense : {stat.defense} \n" +
            $"moveSpeed : {stat.moveSpeed} \n" +
            $"jumpPower : {stat.jumpPower} \n" +
            $"critRate : {stat.critRate} \n" +
            $"critDamage : {stat.critDamage} \n";

        Debug.Log(stat.weapon.equipmentName);
        weapon.sprite = stat.weapon.Image;
        foreach (var data in stat.itemDictionary)
        {
            GameObject go = Object.Instantiate(inventoryItemPrefab, itemTableUI.transform);
            //data.Key
            go.GetComponent<InventoryItem>().SetItem(data.Key);
            go.GetComponent<InventoryItem>().SetStack(data.Value);
        }
    }
}
