using GameCore.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [SerializeField]
    GameObject shopItemPanel;
    List<ShopItem> shopItems;
    public ShopItem selectedItem;
    public List<Item> dataObjects;
    public Button buyButton;
    public GameObject caller;

    private void Start()
    {
        string loadPath = "Equipment/Items";
        dataObjects = GameCore.Managers.Data.LoadScriptableObjects<Item>(loadPath);
        shopItems = new List<ShopItem>();
        shopItems.Add(GameCore.Managers.Resource.Instantiate("UI/SubItem/Item1", shopItemPanel.transform).GetComponent<ShopItem>());
        shopItems.Add(GameCore.Managers.Resource.Instantiate("UI/SubItem/Item1", shopItemPanel.transform).GetComponent<ShopItem>());
        shopItems.Add(GameCore.Managers.Resource.Instantiate("UI/SubItem/Item1", shopItemPanel.transform).GetComponent<ShopItem>());
        AllDeSelected();

        List<Item> selectedData = GetRandomData(shopItems.Count);
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItems[i].item = selectedData[i];
        }

        shopItems.ForEach(item => item.Init());
    }
    public void AllDeSelected()
    {
        foreach (var item in shopItems)
        {
            item.DeSelected();
            item.shopSystem = this;
        }
        buyButton.interactable = false;
    }
    public void SetSelectedItem(ShopItem item)
    {
        selectedItem = item;
        buyButton.interactable = true;
    }
    public Item GetItem()
    {
        if(selectedItem!=null)
        {
            return selectedItem.item;
        }
        return null;
    }

    
    List<Item> GetRandomData(int count)
    {
        List<Item> randomDataList = new List<Item>();

        // �����Ͱ� ������� ���� ��� ���� ó��
        if (count > dataObjects.Count)
        {
            Debug.LogWarning("��û�� �������� ScriptableObject �������� ������ �����ϴ�.");
            count = dataObjects.Count;
        }

        while (randomDataList.Count < count)
        {
            // ScriptableObject ����Ʈ���� �ߺ����� �ʰ� �����ϰ� ����
            Item randomData = dataObjects[Random.Range(0, dataObjects.Count)];

            // �̹� ���õ� ���������� Ȯ��
            if (!randomDataList.Contains(randomData))
            {
                randomDataList.Add(randomData);
            }
        }

        return randomDataList;
    }
}
