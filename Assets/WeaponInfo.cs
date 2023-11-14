using GameCore.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour, IPointerClickHandler
{
    public Weapon weaponScriptable;
    public Image image;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponDescription;
    public GameObject highlight;
    public WeaponSelecter selecter;

    private void Start()
    {
        Init(weaponScriptable.Image, weaponScriptable.name, weaponScriptable.description);
    }
    public void Init(Sprite img, string name, string discription)
    {
        image.sprite = img;
        weaponName.text = name;
        weaponDescription.text = discription;
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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            selecter.AllDeSelected();
            IsSelected();
            selecter.SetSelectedItem(this);
        }
    }
}
