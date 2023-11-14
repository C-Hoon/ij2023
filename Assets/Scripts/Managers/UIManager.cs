using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� UI ����
/// </summary>
namespace GameCore.UI
{
    public class UIManager
    {
        int _order = 10;

        Dictionary<Define.SceneUIType, GameObject> _sceneStack = new Dictionary<Define.SceneUIType, GameObject>();
        Dictionary<Define.PopupUIType, GameObject> _popupStack = new Dictionary<Define.PopupUIType, GameObject>();

        /// <summary>
        /// ��� UI�� �θ� ������Ʈ
        /// </summary>
        public GameObject Root_UI
        {
            get
            {
                GameObject ui = GameObject.Find("@UI");
                if (ui == null)
                {
                    ui = new GameObject { name = "@UI" };
                    ui.transform.position = Vector3.zero;
                    Canvas canvas = ui.AddComponent<Canvas>();
                    ui.AddComponent<GraphicRaycaster>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    canvas.sortingOrder = 0;
                }
                GameObject eventSystem = GameObject.Find("EventSystem");
                if (eventSystem == null)
                {
                    Managers.Resource.Instantiate("UI/EventSystem");
                }
                return ui;
            }
        }
        /// <summary>
        /// ��� SceneUI�� �θ� ������Ʈ
        /// </summary>
        public GameObject Root_Scene
        {
            get
            {
                GameObject root = Root_UI.transform.Find("UI_Scene")?.gameObject;
                if (root == null)
                {
                    root = new GameObject("UI_Scene");
                    var rectTransform = root.AddComponent<RectTransform>();
                    root.transform.SetParent(Root_UI.transform, false);

                    rectTransform.anchorMin = Vector2.zero;
                    rectTransform.anchorMax = Vector2.one;
                    rectTransform.offsetMin = Vector2.zero;
                    rectTransform.offsetMax = Vector2.zero;
                }
                return root;
            }
        }

        /// <summary>
        /// ��� PopupUI�� �θ� ������Ʈ
        /// </summary>
        public GameObject Root_Popup
        {
            get
            {
                GameObject root = Root_UI.transform.Find("UI_Popup")?.gameObject;
                if (root == null)
                {
                    root = new GameObject("UI_Popup");
                    var rectTransform = root.AddComponent<RectTransform>();
                    root.transform.SetParent(Root_UI.transform);

                    rectTransform.anchorMin = Vector2.zero;
                    rectTransform.anchorMax = Vector2.one;
                    rectTransform.offsetMin = Vector2.zero;
                    rectTransform.offsetMax = Vector2.zero;
                }
                return root;
            }
        }

        /// <summary>
        /// ĵ���� ����, SceneUI�� sort = false, PopupUI�� sort = true
        /// </summary>
        /// <param name="go"></param>
        /// <param name="sort"></param>
        public void SetCanvas(GameObject go, bool sort = true)
        {
            Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;

            if (sort)
            {
                canvas.sortingOrder = _order;
                _order++;
            }
            else
            {
                canvas.sortingOrder = 0;
            }
        }

        /// <summary>
        /// WorldSpaceUI���� �Լ�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// 
        /// <example>
        /// WorldSpaceUI�� �����ϴ� �����Դϴ�.
        /// <code>
        /// Managers.UI.MakeWorldSpaceUI<UI_HPBar>(player);
        /// </code>
        /// </example>
        public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");
            if (parent != null)
                go.transform.SetParent(parent);

            Canvas canvas = go.GetOrAddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = Camera.main;

            return Util.GetOrAddComponent<T>(go);
        }

        /// <summary>
        /// SubItem���� �Լ�
        /// </summary>
        /// <typeparam name="T">UI_Base�� ��ӹ޴� Ŭ����</typeparam>
        /// <param name="parent">�θ� UI������Ʈ �Ű�����</param>
        /// <param name="name">���������� �̸�, cs���ϰ� �̸��� ���� �� ������� �ʾƵ� ��</param>
        /// <returns></returns>
        /// 
        /// <example>
        /// SubItem�� �����ϴ� �����Դϴ�.
        /// <code>
        /// Managers.UI.MakeSubItem<UI_Item>(go);
        /// </code>
        /// </example>
        public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
            if (parent != null)
                go.transform.SetParent(parent);

            return Util.GetOrAddComponent<T>(go);
        }

        /// <summary>
        /// SceneUI�� �����ϴ� �ռ�
        /// </summary>
        /// <param name="name">���������� �̸�, cs���ϰ� �̸��� ���� �� ������� �ʾƵ� ��</param>
        /// <returns></returns>
        /// 
        /// <example>
        /// SceneUI�� ����, Ȱ��ȭ�ϴ� �����Դϴ�.
        /// <code>
        /// Managers.UI.ShowSceneUI<UI_Scene_UserPanel>(Define.SceneUIType.UserPanel);
        /// </code>
        /// </example>
        public GameObject ShowSceneUI(Define.SceneUIType sceneUIType, string name)
        {
            GameObject go = Managers.Resource.Instantiate($"UI/Scene/Panel/{name}", Root_Scene.transform);
            if (_sceneStack.ContainsKey(sceneUIType))
                _sceneStack[sceneUIType] = go;
            else
                _sceneStack.Add(sceneUIType, go);
            //go.transform.SetParent(Root_Scene.transform, false);
            return go;
        }

        /// <summary>
        /// PopupUI�� �����ϴ� �Լ�
        /// </summary>
        /// <param name="name">���������� �̸�, cs���ϰ� �̸��� ���� �� ������� �ʾƵ� ��</param>
        /// <returns></returns>
        /// 
        /// <example>
        /// PopupUI�� ����, Ȱ��ȭ�ϴ� �����Դϴ�.
        /// <code>
        /// Managers.UI.ShowPopupUI<UI_Popup_Map>(Define.PopupUIType.Map);
        /// </code>
        /// </example>
        public GameObject ShowPopupUI(Define.PopupUIType popupUIType, string name)
        {
            GameObject go = Managers.Resource.Instantiate($"UI/Popup/Panel/{name}");
            if (_popupStack.ContainsKey(popupUIType))
                _popupStack[popupUIType] = go;
            else
                _popupStack.Add(popupUIType, go);
            go.transform.SetParent(Root_Popup.transform, false);
            Debug.Log($"ShowPopupUI : {popupUIType}, {go}");
            return go;
        }


        /// <summary>
        /// SceneUI Dictionary�� ����� ����UI ����
        /// </summary>
        /// <param name="sceneUIType">Define.SceneUIType�� ����� sceneUI����</param>
        /// 
        /// <example>
        /// ���� SceneUI�� �����ϴ� �����Դϴ�.
        /// <code>
        /// Managers.UI.CloseSceneUI(Define.SceneUIType.UserPanel);
        /// </code>
        /// </example>
        public void CloseSceneUI(Define.SceneUIType sceneUIType)
        {
            if (_sceneStack.Count == 0)
                return;

            if (_sceneStack.ContainsKey(sceneUIType) == false)
            {
                Debug.Log("Close Scene Failed!");
                return;
            }

            GameObject sceneUI = _sceneStack[sceneUIType];
            Managers.Resource.Destroy(sceneUI);
            _sceneStack.Remove(sceneUIType);
            _order--;
        }

        /// <summary>
        /// SceneUI Dictionary�� ����� ���UI ����
        /// </summary>
        /// 
        /// <example>
        /// ��� SceneUI�� �����ϴ� �����Դϴ�.
        /// <code>
        /// Managers.UI.CloseAllSceneUI();
        /// </code>
        /// </example>
        public void CloseAllSceneUI()
        {
            foreach (Define.SceneUIType value in Enum.GetValues(typeof(Define.SceneUIType)))
            {
                CloseSceneUI(value);
            }
        }


        /// <summary>
        /// PopupUI Dictionary�� ����� ����UI ����
        /// </summary>
        /// <param name="popupUIType">Define.PopupUIType�� ����� popupUI����</param>
        /// 
        /// <example>
        /// ���� PopupUI�� �����ϴ� �����Դϴ�.
        /// <code>
        /// Managers.UI.ClosePopupUI(Define.PopupUIType.Map);
        /// </code>
        /// </example>
        public void ClosePopupUI(Define.PopupUIType popupUIType)
        {
            if (_popupStack.Count == 0)
                return;

            if (_popupStack.ContainsKey(popupUIType) == false)
            {
                Debug.Log($"Close Popup Failed! : {popupUIType}, {_popupStack[popupUIType]}");
                return;
            }

            GameObject popupUI = _popupStack[popupUIType];
            Managers.Resource.Destroy(popupUI);
            _popupStack.Remove(popupUIType);
            _order--;
        }

        /// <summary>
        /// PopupUI Dictionary�� ����� ���UI ����
        /// </summary>
        /// 
        /// <example>
        /// ��� PopupUI�� �����ϴ� �����Դϴ�.
        /// <code>
        /// Managers.UI.CloseAllPopupUI();
        /// </code>
        /// </example>
        public void CloseAllPopupUI()
        {
            foreach (Define.PopupUIType value in Enum.GetValues(typeof(Define.PopupUIType)))
            {
                ClosePopupUI(value);
            }
        }

        /// <summary>
        /// ��� SceneUI, PopupUI�� ����
        /// </summary>
        /// 
        /// <example>
        /// ��� UI�� �����ϴ� �����Դϴ�.
        /// <code>
        /// Managers.UI.Clear();
        /// </code>
        /// </example>
        public void Clear()
        {
            CloseAllSceneUI();
            CloseAllPopupUI();
        }
    }
}