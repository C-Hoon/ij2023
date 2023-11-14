using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOpener : ButtonOnClick
{
    public Define.PopupUIType popupUIType;
    public bool pause;

    public override void OnClick()
    {
        GameCore.Managers.UI.ShowPopupUI(popupUIType, popupUIType.ToString());
        GameCore.Managers.Game.SetPause(pause);
    }
}
