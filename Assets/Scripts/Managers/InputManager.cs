using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 입력 제어 관리
/// </summary>
namespace GameCore.InputControl
{
    
    /// <summary>
    /// 기능에 키를 할당해주고 입출력을 관리, 체크하는 InputManager클래스
    /// </summary>
    public class InputManager
    {
        public Dictionary<Define.keyMaps, KeySet> keyMapping;
        public Dictionary<Define.keyMaps, KeySet> GetKeyMappingData { get { return keyMapping; } }

        public float sensitivity = 1f;

        private float axisRaw;
        public float GetAxisRaw { get { return axisRaw; } }

        KeyCode[] defaults = new KeyCode[]
        {
        KeyCode.Z,
        KeyCode.X,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.Space,
        KeyCode.F,
        };

        public void Init()
        {
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            // 불러오기로 교체
            keyMapping = new Dictionary<Define.keyMaps, KeySet>();
            for (int i = 0; i < (int)Define.keyMaps.MaxCount; ++i)
            {
                keyMapping.Add((Define.keyMaps)i, new KeySet(defaults[i]));
            }
        }

        public void Clear()
        {
            keyMapping = new Dictionary<Define.keyMaps, KeySet>();
            for (int i = 0; i < (int)Define.keyMaps.MaxCount; ++i)
            {
                keyMapping.Add((Define.keyMaps)i, new KeySet(defaults[i]));
            }
            //여기에 json으로 키설정 파일을 다시 저장하는 코드 작성
            SaveKeySetting();
        }

        public void SetKeyMap(Define.keyMaps keyMap, KeySet key)
        {
            if (!keyMapping.ContainsKey(keyMap))
                throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap);
            keyMapping[keyMap] = key;
            SaveKeySetting();
        }

        public bool GetKey(Define.keyMaps keyMap)
        {
            return Input.GetKey(keyMapping[keyMap].keyCode);
        }
        public bool GetKeyDown(Define.keyMaps keyMap)
        {
            return Input.GetKeyDown(keyMapping[keyMap].keyCode);
        }
        public bool GetKeyUp(Define.keyMaps keyMap)
        {
            return Input.GetKeyUp(keyMapping[keyMap].keyCode);
        }

        public KeyCode GetKeyInfo(Define.keyMaps keyMap)
        {
            return keyMapping[keyMap].keyCode;
        }

        public void SaveKeySetting()
        {
            //TODO
            //여기에 json으로 키설정 파일을 다시 저장하는 코드 작성
        }

        public void OnUpdate()
        {
            AxisUpdate();
        }

        void AxisUpdate()
        {
            if (GetKey(Define.keyMaps.Left))
                axisRaw = -1f;
            else if (GetKey(Define.keyMaps.Right))
                axisRaw = 1f;
            else
                axisRaw = 0f;
        }
    }
}