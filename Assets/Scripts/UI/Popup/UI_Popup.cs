using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using GameCore.UI;
using System;

public class UI_Popup : UI_Base
{
    Define.PopupUIType popupUIType;
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(popupUIType);
    }
}
