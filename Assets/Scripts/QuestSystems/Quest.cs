using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;



/// <summary>
/// 게임 퀘스트 관련
/// </summary>
namespace QuestSystem
{
    public enum SubscribeType
    {
        PlayerDieEvent,
        EnemyDieEvent,
        GunAttackEvent,
        BladeAttackEvent,
    }

    [Serializable]
    public enum QuestState
    {
        Running,
        Complete,
    }
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest/New Quest")]
    public class Quest : ScriptableObject
    {
        [SerializeField]
        private string codeName;
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string description;

        [SerializeField]
        private Task task;

        public string CodeName => codeName;
        public string DesplayName => displayName;
        public string Description => description;
        public QuestState State { get; private set; }
        public int CurrentSuccess { get { return task.CurrentSuccess; } }
        public int NeedSuccessToComplete { get { return task.NeedSuccessToComplete; } }

        public SubscribeType subscribeType;
        public bool IsComplete => State == QuestState.Complete;

        public void SubscribeToEventBus()
        {
            switch (subscribeType)
            {
                case SubscribeType.BladeAttackEvent:
                    EventBus.Subscribe<BladeAttackEvent>(ReceiveReport);
                    break;
                case SubscribeType.EnemyDieEvent:
                    EventBus.Subscribe<EnemyDieEvent>(ReceiveReport);
                    break;
                case SubscribeType.GunAttackEvent:
                    EventBus.Subscribe<GunAttackEvent>(ReceiveReport);
                    break;
                case SubscribeType.PlayerDieEvent:
                    EventBus.Subscribe<PlayerDieEvent>(ReceiveReport);
                    break;
                default:
                    Debug.Log("subscribeType is null");
                    break;
            }
        }


        /// <summary>
        /// 보고 받았을 때 실행할 함수
        /// </summary>
        /// <param name="successCount"></param>
        public void ReceiveReport(SubscribeEvent successCount)
        {
            Debug.Log($"{displayName} : {task.CurrentSuccess}, {task.NeedSuccessToComplete}");
            task.CurrentSuccess += successCount.value;
            if (task.CurrentSuccess >= task.NeedSuccessToComplete)
            {
                State = QuestState.Complete;
                Complete();
            }
            SaveData();
            GameCore.Managers.Quest.Report(displayName, task.CurrentSuccess, task.NeedSuccessToComplete, IsComplete);
        }

        /// <summary>
        /// Quest를 완료했을 때 실행할 함수
        /// </summary>
        public void Complete()
        {
            //task.Complete();
            //UI 호출

            State = QuestState.Complete;
        }

        public void SaveData()
        {
            //string filePath = $"Assets/Resources/Quest/Save/{displayName}.asset";

            // 기존 파일이 존재하는지 확인
            //QuestSaveData existingData = Resources.Load<QuestSaveData>(filePath);
            //AssetDatabase.LoadAssetAtPath<QuestSaveData>(filePath);

            PlayerPrefs.SetString(DesplayName, CodeName);
            PlayerPrefs.SetInt(DesplayName, task.CurrentSuccess);
            PlayerPrefs.SetInt($"{DesplayName}State", (int)State);


            



            /*string codeName = PlayerPrefs.GetString(DesplayName);
            int taskSuccessValue = PlayerPrefs.GetInt(DesplayName);
            if (taskSuccessValue >= task.NeedSuccessToComplete)
                State = QuestState.Complete;
            else
                State = QuestState.Running;*/

            /*if (existingData != null)
            {
                // 기존 파일이 있으면 값을 수정
                existingData.codeName = this.codeName;
                existingData.state = this.State;
                existingData.taskSuccessValue = task.CurrentSuccess;

                // 수정된 데이터를 저장
                EditorUtility.SetDirty(existingData);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            else
            {
                // 기존 파일이 없으면 새 파일 생성
                QuestSaveData questSaveData = new QuestSaveData { codeName = this.codeName, state = this.State, taskSuccessValue = task.CurrentSuccess };
                AssetDatabase.CreateAsset(questSaveData, filePath);
            }*/

            //QuestSaveData questSaveData = new QuestSaveData { codeName = this.codeName, state = this.State, taskSuccessValue = task.CurrentSuccess };
            //AssetDatabase.CreateAsset(questSaveData, $"Assets/Resources/Quest/Save/{displayName}.asset");
        }
        public void LoadFrom(QuestSaveData saveData)
        {
            /*State = saveData.state;
            task.CurrentSuccess = saveData.taskSuccessValue;*/
            task.CurrentSuccess = PlayerPrefs.GetInt(DesplayName);
            State = (QuestState)PlayerPrefs.GetInt($"{DesplayName}State");
        }
    }
}

