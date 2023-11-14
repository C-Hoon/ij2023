using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����Ʈ ����
/// </summary>
namespace QuestSystem
{
    public class GameObjectTarget : TaskTarget
    {
        private GameObject value;

        public override object Value => value;


        public override bool IsEqual(object target)
        {
            GameObject targetAsGameObject = target as GameObject;
            if (targetAsGameObject == null)
                return false;
            return targetAsGameObject.name.Contains(value.name);
        }
    }
}