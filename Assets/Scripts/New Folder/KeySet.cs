using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 키의 정보를 저장하는 클래스
/// </summary>
public class KeySet
{
    public KeyCode keyCode;
    public KeySet(KeyCode key)
    {
        keyCode = key;
    }
}
