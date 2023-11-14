using QuestSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameCore.Resource
{
    /// <summary>
    /// 데이터를 파싱해 읽어내거나 저장하는 기능을 가지는 DataManager클래스
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

            // ScriptableObject 파일을 로드하여 리스트에 추가
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

            // "Resources" 폴더 내에서 ScriptableObject 파일들을 로드
            T[] loadedObjects = Resources.LoadAll<T>(path);

            foreach (T loadedObject in loadedObjects)
            {
                loadedScriptableObjects.Add(loadedObject);
            }

            return loadedScriptableObjects;
        }
    }
    
}
