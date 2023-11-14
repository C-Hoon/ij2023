using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using UnityEngine.UI;
using TMPro;


public class InputManagerTest : BaseScene
{
    string _button = "Fire1";

    void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();
        //SceneType = Define.Scene.PlayerTest;
        Debug.Log("InputManagerTest Init");

        //Managers.Resource.Instantiate("Character/Player");
        //Managers.Resource.Instantiate("Environment/Floor");
        Managers.UI.ShowPopupUI(Define.PopupUIType.KeySetting, "KeySetting");
    }

    private void Update()
    {
        if (Managers.Input.GetKeyDown(Define.keyMaps.Attack))
            Debug.Log(Managers.Input.GetKeyInfo(Define.keyMaps.Attack));

        if (Input.GetMouseButtonDown(0))
        {
            // 클릭된 위치의 스크린 좌표를 Ray로 변환
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 클릭된 오브젝트의 정보를 출력
                Debug.Log("Clicked Object: " + hit.collider.gameObject.name);
            }
            Debug.Log("Click");
        }
        
    }
    public override void Clear()
    {
        
    }
}
