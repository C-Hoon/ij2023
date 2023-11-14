using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 퀘스트 관련
/// </summary>
namespace QuestSystem
{
    /// <summary>
    /// 성공값에 양수가 들어올 경우 더하고 음수일 경우 현재 성공횟수를 0으로 초기화
    /// </summary>
    public class ContinuousCount : TaskAction
    {
        public override int Run(Task task, int currentSuccess, int successCount)
        {
            return successCount > 0 ? currentSuccess + successCount : 0 ;
        }
    }
}
