using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyEvent : ButtonOnClick
{
    [SerializeField]
    Define.PopupUIType popupUI;
    public bool pause;
    public ShopSystem shopSystem;
    public override void OnClick()
    {
        PlayerStat playerStat = GameCore.Managers.Game.Player.GetComponent<PlayerMove>().playerStat;

        playerStat.AddToItemList(shopSystem.selectedItem.item);
        GameCore.Managers.Game.SetPause(false);
        if (shopSystem.caller != null)
        {
            Destroy(shopSystem.caller);
        }
        GameCore.Managers.UI.ClosePopupUI(Define.PopupUIType.ShopUI);
        
    }
}
