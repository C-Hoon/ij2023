using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 시작할때 초기화작업
    /// </summary>
    protected virtual void Init()
    {
        
    }

    /// <summary>
    /// 씬을 전환할 때 지워야 할 것들 처리
    /// </summary>
    public abstract void Clear();
}
