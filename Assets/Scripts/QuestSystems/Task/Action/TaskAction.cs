using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TaskActionType
{
    ContinuousCount, 
    NegativeCount, 
    PositiveCount, 
    SimpleCount, 
    SimpleSet
}
/// <summary>
/// 게임 퀘스트 관련
/// </summary>
namespace QuestSystem
{

    public abstract class TaskAction
    {
        public abstract int Run(Task task, int currentSuccess, int successCount);
    }

    public class TaskActionFactory
    {
        public static TaskAction CreateTaskAction(TaskActionType actionType)
        {
            switch (actionType)
            {
                case TaskActionType.ContinuousCount:
                    return new ContinuousCount();
                case TaskActionType.NegativeCount:
                    return new NegativeCount();
                case TaskActionType.PositiveCount:
                    return new PositiveCount();
                case TaskActionType.SimpleCount:
                    return new SimpleCount();
                case TaskActionType.SimpleSet:
                    return new SimpleSet();
                default:
                    throw new ArgumentException("Invalid action type: " + actionType);
            }
        }
    }
}