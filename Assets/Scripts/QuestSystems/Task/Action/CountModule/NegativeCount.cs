using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 퀘스트 관련
/// </summary>
namespace QuestSystem
{
    /// <summary>
    /// 성공값에 음수가 들어올 경우 더함
    /// </summary>
    public class NegativeCount : TaskAction
    {
        public override int Run(Task task, int currentSuccess, int successCount)
        {
            return successCount < 0 ? currentSuccess - successCount : successCount;
        }
    }
}

