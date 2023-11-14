using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// �Է� ���� ����
/// </summary>
namespace GameCore.InputControl
{
    
    /// <summary>
    /// ��ɿ� Ű�� �Ҵ����ְ� ������� ����, üũ�ϴ� InputManagerŬ����
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
            // �ҷ������ ��ü
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
            //���⿡ json���� Ű���� ������ �ٽ� �����ϴ� �ڵ� �ۼ�
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
            //���⿡ json���� Ű���� ������ �ٽ� �����ϴ� �ڵ� �ۼ�
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