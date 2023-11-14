using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : ButtonOnClick
{
    [SerializeField]
    private Define.Scene SceneType = Define.Scene.Game;
    public override void OnClick()
    {
        SceneManager.LoadScene(SceneType.ToString());
    }
}
