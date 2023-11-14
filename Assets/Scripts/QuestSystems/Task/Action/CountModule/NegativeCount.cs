using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����Ʈ ����
/// </summary>
namespace QuestSystem
{
    /// <summary>
    /// �������� ������ ���� ��� ����
    /// </summary>
    public class NegativeCount : TaskAction
    {
        public override int Run(Task task, int currentSuccess, int successCount)
        {
            return successCount < 0 ? currentSuccess - successCount : successCount;
        }
    }
}

