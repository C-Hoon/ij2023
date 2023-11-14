using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using TMPro;

public class StartScene : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textComp;
    void Start()
    {
        Init();
    }

    void Init()
    {
        Debug.Log("StageTest Init");
        GameObject go = Managers.UI.ShowSceneUI(Define.SceneUIType.Start_Panel, "Start_Panel");
        textComp.text = $"{go.name}";
    }
}
