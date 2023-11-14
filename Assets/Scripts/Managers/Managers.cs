using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Resource;
using GameCore.InputControl;
using GameCore.QuestSystem;
using GameCore.UI;

/// <summary>
/// ���� ��� �ھ�
/// </summary>
namespace GameCore
{
    /// <summary>
    /// �̱������� ������ ������ ����� �����ϴ� ���� �Ŵ���
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
        /// ResourceManager�� �����ϱ� ���� ������Ƽ
        /// </summary>
        public static ResourceManager Resource { get { return Instance._resource; } }
        /// <summary>
        /// SoundManager �����ϱ� ���� ������Ƽ
        /// </summary>
        public static SoundManager Sound { get { return Instance._sound; } }
        /// <summary>
        /// InputManager �����ϱ� ���� ������Ƽ
        /// </summary>
        public static InputManager Input { get { return Instance._input; } }
        /// <summary>
        /// UIManager �����ϱ� ���� ������Ƽ
        /// </summary>
        public static UIManager UI { get { return Instance._ui; } }
        /// <summary>
        /// DataManager �����ϱ� ���� ������Ƽ
        /// </summary>
        public static DataManager Data { get { return Instance._data; } }
        /// <summary>
        /// QuestManager �����ϱ� ���� ������Ƽ
        /// </summary>
        public static QuestManager Quest { get { return Instance._quest; } }
        /// <summary>
        /// SceneManager �����ϱ� ���� ������Ƽ
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
        /// �ʱ�ȭ �۾�, ������ �ʱ�ȭ�� �̱��� ��ü�� �����ϹǷ� �̱��� ��ü�� �̲����� ��ũ��Ʈ�� �ʿ�
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
        /// ������ Ŭ���� �Լ�
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

