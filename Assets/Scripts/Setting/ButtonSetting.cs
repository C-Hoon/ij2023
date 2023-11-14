using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameCore;
using System.Linq;

public class ButtonSetting : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    Button button;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    Define.keyMaps keyMap;

    GameObject panel;
    private void Start()
    {
        button.onClick.AddListener(OnClick);
        ChangeText();
    }
    void OnClick()
    {
        panel = Managers.Resource.Instantiate("UI/Popup/Panel/Panel", Managers.UI.Root_Popup.transform);
        //panel = Managers.UI.ShowPopupUI(Define.PopupUIType.Panel, "Panel");
        WaitText();
        InputDetection();
    }
    void WaitText()
    {
        text.text = "버튼을 입력하세요...";
    }
    void InputDetection()
    {
        StartCoroutine(WaitForInput());
    }
    void ChangeText()
    {
        title.text = keyMap.ToString();
        text.text = Managers.Input.GetKeyInfo(keyMap).ToString();
    }

    IEnumerator WaitForInput()
    {
        while (true)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    Managers.Resource.Destroy(panel);
                    text.text = Managers.Input.GetKeyInfo(keyMap).ToString();
                    yield break;
                }
                else if (Input.GetKey(keyCode))
                {
                    foreach(KeySet keyset in Managers.Input.GetKeyMappingData.Values.ToList())
                    {
                        if (keyset.keyCode == keyCode)
                        {
                            Managers.Resource.Destroy(panel);
                            text.text = Managers.Input.GetKeyInfo(keyMap).ToString();
                            yield break;
                        }
                    }
                    Managers.Resource.Destroy(panel);
                    Debug.Log("Key " + keyCode.ToString() + " is currently being held down.");
                    Managers.Input.SetKeyMap(keyMap, new KeySet(keyCode));
                    text.text = Managers.Input.GetKeyInfo(keyMap).ToString();
                    yield break;
                }
            }
            yield return null;
        }
    }
}
