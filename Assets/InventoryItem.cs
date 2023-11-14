using GameCore.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Item item;
    public int stack;
    public TextMeshProUGUI stackText;
    private bool isMouseOver = false;

    [SerializeField]
    GameObject overlayUIPrefab;
    GameObject overlayUI;

    public void SetItem(Item data)
    {
        item = data;
        //
        GetComponent<Image>().sprite = item.Image;
    }
    public void SetStack(int data)
    {
        stack = data;
        stackText.text = stack.ToString();
    }
}
