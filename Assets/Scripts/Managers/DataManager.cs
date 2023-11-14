using QuestSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameCore.Resource
{
    /// <summary>
    /// �����͸� �Ľ��� �о�ų� �����ϴ� ����� ������ DataManagerŬ����
    /// </summary>
    public class DataManager
    {
        public GameData gameData;
        public UserData userData;
        public void Init()
        {
            string gameDataPath = "Prefabs/DataScriptable/data";
            //string gameDataPath = "Assets/Resources/Prefabs/DataScriptable/data.asset";
            //gameData = AssetDatabase.LoadAssetAtPath<GameData>(gameDataPath);
            gameData = Resources.Load<GameData>(gameDataPath);
            string userDataPath = "Prefabs/DataScriptable/data";
            //string userDataPath = "Assets/Resources/Prefabs/DataScriptable/data.asset";
            //userData = AssetDatabase.LoadAssetAtPath<UserData>(userDataPath);
            userData = Resources.Load<UserData>(userDataPath);
        }
        /*public void SaveUserData()
        {
            string assetPath = "Assets/Resources/Prefabs/DataScriptable/save.asset";
            if (userData == null)
            {
                userData = ScriptableObject.CreateInstance<UserData>();
            }
            AssetDatabase.CreateAsset(userData, assetPath);
            EditorUtility.SetDirty(userData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }*/
        public List<T> LoadScriptableObjects<T>(string path) where T : ScriptableObject
        {
            /*List<T> loadedScriptableObjects = new List<T>();
            string[] scriptableObjectPaths = Directory.GetFiles(path, "*.asset");

            // ScriptableObject ������ �ε��Ͽ� ����Ʈ�� �߰�
            foreach (string objectPath in scriptableObjectPaths)
            {
                //T loadedObject = (T)UnityEditor.AssetDatabase.LoadAssetAtPath(objectPath, typeof(T));
                T loadedObject = Resources.Load<T>(objectPath);
                if (loadedObject != null)
                {
                    loadedScriptableObjects.Add(loadedObject);
                }
            }
            return loadedScriptableObjects;*/
            List<T> loadedScriptableObjects = new List<T>();

            // "Resources" ���� ������ ScriptableObject ���ϵ��� �ε�
            T[] loadedObjects = Resources.LoadAll<T>(path);

            foreach (T loadedObject in loadedObjects)
            {
                loadedScriptableObjects.Add(loadedObject);
            }

            return loadedScriptableObjects;
        }
    }
    
}
