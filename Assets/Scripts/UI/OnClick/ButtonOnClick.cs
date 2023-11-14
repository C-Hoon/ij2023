using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonOnClick : MonoBehaviour
{
    private void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public abstract void OnClick();
}
