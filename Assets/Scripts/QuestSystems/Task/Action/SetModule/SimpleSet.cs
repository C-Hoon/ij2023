using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 게임 퀘스트 관련
/// </summary>
namespace QuestSystem
{
    /// <summary>
    /// 성공값을 현재 성공값에 대입
    /// </summary>
    public class SimpleSet : TaskAction
    {
        public override int Run(Task task, int currentSuccess, int successCount)
        {
            return successCount;
        }
    }
}