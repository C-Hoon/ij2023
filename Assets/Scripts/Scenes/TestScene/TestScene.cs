using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
public class TestScene : BaseScene
{
    public GameObject[] stages;
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.TestScene;
        Debug.Log("TestScene Init");

        int totalPoint = Managers.Game.totalPoint;
        Managers.Sound.SetVolume(0.1f);
        Managers.Sound.Play("BGM/BGM_01", Define.Sound.Bgm);
        Managers.UI.ShowSceneUI(Define.SceneUIType.State_Panel, "State_Panel");
        Managers.UI.ShowSceneUI(Define.SceneUIType.Info, "Info");
        Managers.UI.ShowSceneUI(Define.SceneUIType.Pause, "Pause");
        Managers.Game.Stages = stages;
    }
    private void Update()
    {
        if (Managers.Input.GetKeyDown(Define.keyMaps.Attack))
            Debug.Log(Managers.Input.GetKeyInfo(Define.keyMaps.Attack));

        if (Input.GetMouseButtonDown(0))
        {
            // Ŭ���� ��ġ�� ��ũ�� ��ǥ�� Ray�� ��ȯ
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Ŭ���� ������Ʈ�� ������ ���
                Debug.Log("Clicked Object: " + hit.collider.gameObject.name);
            }
            Debug.Log("Click");
        }

    }
    public override void Clear()
    {

    }
}
