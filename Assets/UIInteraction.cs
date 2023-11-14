using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInteraction : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI keyUI;
    [SerializeField]
    Define.PopupUIType popupUIType;
    public bool pause;
    private void Start()
    {
        string key = GameCore.Managers.Input.keyMapping[Define.keyMaps.Interaction].keyCode.GetString();
        keyUI.text = key.ToUpper();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameCore.Managers.Game.isPaused)
            return;
        if (GameCore.Managers.Input.GetKeyDown(Define.keyMaps.Interaction))
        {
            GameObject go = GameCore.Managers.UI.ShowPopupUI(popupUIType, popupUIType.ToString());
            ShopSystem shop = go.GetComponent<ShopSystem>();
            shop.caller = transform.parent.gameObject;
            GameCore.Managers.Game.SetPause(pause);
        }
    }
}
