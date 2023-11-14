using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 퀘스트 관련
/// </summary>
namespace QuestSystem
{

    public abstract class TaskTarget
    {
        public abstract object Value { get; }
        public abstract bool IsEqual(object target);
    }
}