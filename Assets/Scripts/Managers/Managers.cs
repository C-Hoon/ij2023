using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Resource;
using GameCore.InputControl;
using GameCore.QuestSystem;
using GameCore.UI;

/// <summary>
/// 게임 기능 코어
/// </summary>
namespace GameCore
{
    /// <summary>
    /// 싱글톤으로 구현된 게임의 기능을 제어하는 메인 매니저
    /// </summary>
    public class Managers : MonoBehaviour
    {
        static Managers s_instance;
        static Managers Instance { get { Init(); return s_instance; } }


        #region Core

        ResourceManager _resource = new ResourceManager();
        SoundManager _sound = new SoundManager();
        InputManager _input = new InputManager();
        UIManager _ui = new UIManager();
        DataManager _data = new DataManager();
        QuestManager _quest = new QuestManager();
        GameManager _game = new GameManager();
        //SceneManager _scene = new SceneManager();

        /// <summary>
        /// ResourceManager에 접근하기 위한 프로퍼티
        /// </summary>
        public static ResourceManager Resource { get { return Instance._resource; } }
        /// <summary>
        /// SoundManager 접근하기 위한 프로퍼티
        /// </summary>
        public static SoundManager Sound { get { return Instance._sound; } }
        /// <summary>
        /// InputManager 접근하기 위한 프로퍼티
        /// </summary>
        public static InputManager Input { get { return Instance._input; } }
        /// <summary>
        /// UIManager 접근하기 위한 프로퍼티
        /// </summary>
        public static UIManager UI { get { return Instance._ui; } }
        /// <summary>
        /// DataManager 접근하기 위한 프로퍼티
        /// </summary>
        public static DataManager Data { get { return Instance._data; } }
        /// <summary>
        /// QuestManager 접근하기 위한 프로퍼티
        /// </summary>
        public static QuestManager Quest { get { return Instance._quest; } }
        /// <summary>
        /// SceneManager 접근하기 위한 프로퍼티
        /// </summary>
        //public static SceneManager Scene { get { return Instance._scene; } }

        public static GameManager Game { get { return Instance._game; } }
        #endregion



        void Start()
        {
            Init();
        }


        void Update()
        {
            Input.OnUpdate();
        }

        /// <summary>
        /// 초기화 작업, 게으른 초기화로 싱글톤 객체를 생성하므로 싱글톤 객체를 이끌어줄 스크립트가 필요
        /// </summary>
        static void Init()
        {
            if (s_instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject("@Managers");
                    go.AddComponent<Managers>();
                }
                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<Managers>();

                s_instance._game.Init();
                s_instance._input.Init();
                s_instance._data.Init();
                //s_instance._pool.Init();
                s_instance._sound.Init();
                s_instance._quest.Init();
            }
        }

        /// <summary>
        /// 데이터 클리어 함수
        /// </summary>
        static void Clear()
        {
            Input.Clear();
            Sound.Clear();
            //Scene.Clear();
            UI.Clear();
            Quest.Clear();
        }
    }
}

