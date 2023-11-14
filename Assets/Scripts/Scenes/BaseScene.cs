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
    /// �����Ҷ� �ʱ�ȭ�۾�
    /// </summary>
    protected virtual void Init()
    {
        
    }

    /// <summary>
    /// ���� ��ȯ�� �� ������ �� �͵� ó��
    /// </summary>
    public abstract void Clear();
}
