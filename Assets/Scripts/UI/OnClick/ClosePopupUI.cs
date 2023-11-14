using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePopupUI : ButtonOnClick
{
    [SerializeField]
    Define.PopupUIType popupUI;
    public bool pause;
    public override void OnClick()
    {
        GameCore.Managers.Game.SetPause(pause);
        GameCore.Managers.UI.ClosePopupUI(popupUI);
    }
}
