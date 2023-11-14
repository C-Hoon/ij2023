using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExitEvent : ButtonOnClick
{
    public override void OnClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
