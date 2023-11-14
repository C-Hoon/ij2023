using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

public class UITest : BaseScene
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.UITest;
        Debug.Log("UITest Init");

        /*GameObject Scene = Managers.Resource.Instantiate("UI/Scene/UI_Scene");
        GameObject UserPanel = Managers.Resource.Instantiate("UI/Scene/UI_Scene_UserPanel", Scene.transform);
        GameObject MiniMap = Managers.Resource.Instantiate("UI/Scene/UI_Scene_MiniMap", Scene.transform);

        Managers.Resource.Instantiate("UI/Scene/UserPanel/UI_Scene_HPBar", UserPanel.transform);
        Managers.Resource.Instantiate("UI/Scene/UserPanel/UI_Scene_MPBar", UserPanel.transform);*/


        //Managers.UI.ShowSceneUI<UI_Scene_UserPanel>(Define.SceneUIType.UserPanel);
        //Managers.UI.ShowSceneUI<UI_Scene_MiniMap>(Define.SceneUIType.MiniMap);
        //Managers.UI.MakeWorldSpaceUI<UI_HPBar>();


        //Managers.UI.ShowPopupUI<UI_Button>(Define.PopupUIType.Button);


        //StartCoroutine(UIClear(3.0f));
    }

    private IEnumerator UIClear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); //waitTime 만큼 딜레이후 다음 코드가 실행된다.
        Managers.UI.Clear();
    }

    public override void Clear()
    {
        
    }
}
