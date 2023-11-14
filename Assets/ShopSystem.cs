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

        // 데이터가 충분하지 않을 경우 예외 처리
        if (count > dataObjects.Count)
        {
            Debug.LogWarning("요청한 개수보다 ScriptableObject 데이터의 개수가 적습니다.");
            count = dataObjects.Count;
        }

        while (randomDataList.Count < count)
        {
            // ScriptableObject 리스트에서 중복되지 않게 랜덤하게 선택
            Item randomData = dataObjects[Random.Range(0, dataObjects.Count)];

            // 이미 선택된 데이터인지 확인
            if (!randomDataList.Contains(randomData))
            {
                randomDataList.Add(randomData);
            }
        }

        return randomDataList;
    }
}
