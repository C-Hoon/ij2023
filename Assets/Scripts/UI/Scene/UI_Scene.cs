using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using GameCore.UI;

public class UI_Scene : UI_Base
{
	Define.SceneUIType sceneUIType;
	public override void Init()
	{
		Managers.UI.SetCanvas(gameObject, false);
	}
	public virtual void CloseSceneUI()
	{
		Managers.UI.CloseSceneUI(sceneUIType);
	}
}
