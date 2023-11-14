using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// ���� ����Ʈ ����
/// </summary>
namespace QuestSystem
{
    public enum TaskState
    {
        Running,
        Complete
    }
    /// <summary>
    /// ����Ʈ�ý����� Task
    /// </summary>
    [CreateAssetMenu(fileName = "Task", menuName = "Quest/New Task")]
    public class Task : ScriptableObject
    {
        [SerializeField]
        private int needSuccessToComplete;

        private int currentSuccess;
        /// <summary>
        /// ����Ʈ�� ���� ����Ƚ��
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
        /// ����Ʈ�� �����ϱ� ���� ����Ƚ��
        /// </summary>
        public int NeedSuccessToComplete => needSuccessToComplete;
    }
}
