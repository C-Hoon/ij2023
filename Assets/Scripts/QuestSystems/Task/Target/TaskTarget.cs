using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����Ʈ ����
/// </summary>
namespace QuestSystem
{

    public abstract class TaskTarget
    {
        public abstract object Value { get; }
        public abstract bool IsEqual(object target);
    }
}