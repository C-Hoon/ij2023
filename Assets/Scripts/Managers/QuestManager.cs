using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QuestSystem;


namespace GameCore.QuestSystem
{
    public class QuestManager
    {
        #region Events
        public delegate void QuestRegisteredHandler(Quest newQuest);
        public delegate void QuestCompletedHandler(Quest quest);
        #endregion
        

        //초기화, 저장된 세이브 불러오기
        public void Init()
        {
            List<Quest> quests = GameCore.Managers.Data.LoadScriptableObjects<Quest>("Quest");
            List<QuestSaveData> saveDatas = GameCore.Managers.Data.LoadScriptableObjects<QuestSaveData>("Quest/Save");

            foreach (var quest in quests)
            {
                foreach(var save in saveDatas)
                {
                    if(save.codeName == quest.CodeName)
                    {
                        quest.LoadFrom(save);
                        break;
                    }
                }

                if(quest.State == QuestState.Running)
                {
                    activeAchievements.Add(quest);
                }
                else if (quest.State == QuestState.Complete)
                {
                    completedAchievements.Add(quest);
                }
            }

            foreach(var achievement in activeAchievements)
            {
                achievement.SubscribeToEventBus();
            }
        }
        public void Clear()
        {
            activeAchievements.Clear();
            completedAchievements.Clear();
        }

        private List<Quest> activeAchievements = new List<Quest>();
        private List<Quest> completedAchievements = new List<Quest>();

        public event QuestRegisteredHandler onQuestRegistered;
        public event QuestCompletedHandler onQuestCompleted;

        public IReadOnlyList<Quest> ActiveAchievements => activeAchievements;
        public IReadOnlyList<Quest> CompletedAchievements => completedAchievements;

        public void Register(Quest quest)
        {
            //var newQuest = Instantiate(quest);
        }

        public void QuestCompleted()
        {
            //있는지 확인하고 CompletedAchievements로 보내버리기
        }
        public void Report(string displayName, int current, int max, bool completed)
        {
            EventBus.Publish(new QuestUIEvent { name = displayName, currentSuccess = current, needSuccessToComplete = max, isCompleted = completed });
        }
    }
}