using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 UI 관리
/// </summary>
namespace GameCore.UI
{
    public class UIManager
    {
        int _order = 10;

        Dictionary<Define.SceneUIType, GameObject> _sceneStack = new Dictionary<Define.SceneUIType, GameObject>();
        Dictionary<Define.PopupUIType, GameObject> _popupStack = new Dictionary<Define.PopupUIType, GameObject>();

        /// <summary>
        /// 모든 UI의 부모 오브젝트
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
        /// 모든 SceneUI의 부모 오브젝트
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
        /// 모든 PopupUI의 부모 오브젝트
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
        /// 캔버스 설정, SceneUI는 sort = false, PopupUI는 sort = true
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
        /// WorldSpaceUI생성 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// 
        /// <example>
        /// WorldSpaceUI를 생성하는 예시입니다.
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
        /// SubItem생성 함수
        /// </summary>
        /// <typeparam name="T">UI_Base를 상속받는 클래스</typeparam>
        /// <param name="parent">부모 UI오브젝트 매개변수</param>
        /// <param name="name">프리팹파일 이름, cs파일과 이름이 같을 시 사용하지 않아도 됨</param>
        /// <returns></returns>
        /// 
        /// <example>
        /// SubItem을 생성하는 예시입니다.
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
        /// SceneUI를 생성하는 합수
        /// </summary>
        /// <param name="name">프리팹파일 이름, cs파일과 이름이 같을 시 사용하지 않아도 됨</param>
        /// <returns></returns>
        /// 
        /// <example>
        /// SceneUI를 생성, 활성화하는 예시입니다.
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
        /// PopupUI를 생성하는 함수
        /// </summary>
        /// <param name="name">프리팹파일 이름, cs파일과 이름이 같을 시 사용하지 않아도 됨</param>
        /// <returns></returns>
        /// 
        /// <example>
        /// PopupUI를 생성, 활성화하는 예시입니다.
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
        /// SceneUI Dictionary에 저장된 지정UI 제거
        /// </summary>
        /// <param name="sceneUIType">Define.SceneUIType에 선언된 sceneUI종류</param>
        /// 
        /// <example>
        /// 지정 SceneUI를 제거하는 예시입니다.
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
        /// SceneUI Dictionary에 저장된 모든UI 제거
        /// </summary>
        /// 
        /// <example>
        /// 모든 SceneUI를 제거하는 예시입니다.
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
        /// PopupUI Dictionary에 저장된 지정UI 제거
        /// </summary>
        /// <param name="popupUIType">Define.PopupUIType에 선언된 popupUI종류</param>
        /// 
        /// <example>
        /// 지정 PopupUI를 제거하는 예시입니다.
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
        /// PopupUI Dictionary에 저장된 모든UI 제거
        /// </summary>
        /// 
        /// <example>
        /// 모든 PopupUI를 제거하는 예시입니다.
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
        /// 모든 SceneUI, PopupUI를 제거
        /// </summary>
        /// 
        /// <example>
        /// 모든 UI를 제거하는 예시입니다.
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