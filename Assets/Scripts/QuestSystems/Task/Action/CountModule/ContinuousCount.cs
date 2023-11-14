using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����Ʈ ����
/// </summary>
namespace QuestSystem
{
    /// <summary>
    /// �������� ����� ���� ��� ���ϰ� ������ ��� ���� ����Ƚ���� 0���� �ʱ�ȭ
    /// </summary>
    public class ContinuousCount : TaskAction
    {
        public override int Run(Task task, int currentSuccess, int successCount)
        {
            return successCount > 0 ? currentSuccess + successCount : 0 ;
        }
    }
}
