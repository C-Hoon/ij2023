using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// 게임 퀘스트 관련
/// </summary>
namespace QuestSystem
{
    public enum TaskState
    {
        Running,
        Complete
    }
    /// <summary>
    /// 퀘스트시스템의 Task
    /// </summary>
    [CreateAssetMenu(fileName = "Task", menuName = "Quest/New Task")]
    public class Task : ScriptableObject
    {
        [SerializeField]
        private int needSuccessToComplete;

        private int currentSuccess;
        /// <summary>
        /// 퀘스트의 현재 성공횟수
        /// </summary>
        public int CurrentSuccess
        {
            get => currentSuccess;
            set
            {
                currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);
            }
        }
        /// <summary>
        /// 퀘스트가 성공하기 위한 성공횟수
        /// </summary>
        public int NeedSuccessToComplete => needSuccessToComplete;
    }
}
