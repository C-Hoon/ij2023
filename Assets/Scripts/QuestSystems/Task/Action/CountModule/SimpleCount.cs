using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 퀘스트 관련
/// </summary>
namespace QuestSystem
{
    /// <summary>
    /// 현재 성공값에 추가 성공값을 더함
    /// </summary>
    public class SimpleCount : TaskAction
    {
        public override int Run(Task task, int currentSuccess, int successCount)
        {
            return currentSuccess + successCount;
        }
    }
}