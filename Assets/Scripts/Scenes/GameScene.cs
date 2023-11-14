using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

public class GameScene : BaseScene
{
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        Debug.Log("GameScene Init");

        Managers.Resource.Instantiate("Character/Player");
        Managers.Resource.Instantiate("Environment/Floor");

        Managers.Sound.Play("BGM/BGM_01", Define.Sound.Bgm);
        //Managers.Sound.Play("EFFECT/EFFECT_01");

        Managers.UI.ShowSceneUI(Define.SceneUIType.State_Panel, "State_Panel");
        Managers.UI.ShowSceneUI(Define.SceneUIType.Info, "Info");
        Managers.UI.ShowSceneUI(Define.SceneUIType.Pause, "Pause");
    }

    public override void Clear()
    {

    }
}
