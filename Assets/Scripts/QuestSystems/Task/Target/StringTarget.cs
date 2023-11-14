using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 퀘스트 관련
/// </summary>
namespace QuestSystem
{
    public class StringTarget : TaskTarget
    {
        private string value;

        public override object Value => value;

        public override bool IsEqual(object target)
        {
            string targetAsString = target as string;
            if (targetAsString == null)
                return false;
            return targetAsString == value;
        }
    }
}