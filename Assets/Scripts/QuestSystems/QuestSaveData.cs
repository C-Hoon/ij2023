using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{

    [CreateAssetMenu(fileName = "QuestSaveData", menuName = "Quest/New QuestSaveData")]
    public class QuestSaveData : ScriptableObject
    {
        public string codeName;
        public QuestState state;
        public int taskSuccessValue;
    }
}