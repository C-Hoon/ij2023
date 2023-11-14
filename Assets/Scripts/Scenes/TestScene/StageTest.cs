using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
public class StageTest : BaseScene
{
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.StageTest;
        Debug.Log("StageTest Init");

        //Managers.Resource.Instantiate("Character/Player");

        
    }

    public override void Clear()
    {

    }
}
