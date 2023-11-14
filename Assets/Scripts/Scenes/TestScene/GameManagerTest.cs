using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
public class GameManagerTest : BaseScene
{
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameManagerTest;
        Debug.Log("GameManagerTest Init");

        Managers.Resource.Instantiate("Character/Player");
        Managers.Resource.Instantiate("Environment/Floor");
    }

    public override void Clear()
    {

    }
}
