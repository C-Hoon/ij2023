using GameCore.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public ShopSystem shopSystem;

    public Item item;
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI description;
    public GameObject highlight;

    public  void Init()
    {
        //itemName.font = "";
        //description.font = "";

        itemImage.sprite = item.Image;
        itemName.text = item.equipmentName;
        description.text = item.description;
    }
    public void IsSelected()
    {
        highlight.SetActive(true);
    }
    public void DeSelected()
    {
        highlight.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            shopSystem.AllDeSelected();
            IsSelected();
            shopSystem.SetSelectedItem(this);
        }
    }
}
