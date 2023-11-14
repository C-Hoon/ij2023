using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����Ʈ ����
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