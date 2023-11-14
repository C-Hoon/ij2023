using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;
using System;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class QuestGenerator : MonoBehaviour
{
    public Quest_Generation quest;
    [SerializeField]
    Button generationButton;
    void Start()
    {
        generationButton.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        Quest_SaveAsJson(quest);
    }

    void Quest_SaveAsJson(Quest_Generation quest)
    {
        if(quest.codeName == "")
        {
            Debug.Log("퀘스트 코드네임이 공백입니다.");
            return;
        }
        else if (GameCore.Managers.Resource.Load<TextAsset>($"Data/Quest/{quest.codeName}") != null)
        {
            Debug.Log("중복된 퀘스트 코드네임입니다.");
            return;
        }
        string json = JsonUtility.ToJson(quest);
        System.IO.File.WriteAllText($"Assets/Resources/Data/Quest/{quest.codeName}.json", json);
        Debug.Log("Json파일이 생성되었습니다.");

    }
}
[Serializable]
public class Quest_Generation
{
    [Header("기본정보")]
    [Tooltip("카테고리")]
    public Category_Generation category;
    [Tooltip("퀘스트 아이콘의 주소")]
    public string iconPath;
    [Tooltip("퀘스트의 코드네임(예: FirstQuest_Quest, KillSlime_Quest)")]
    public string codeName;
    [Tooltip("퀘스트가 게임상에 표시될 제목")]
    public string displayName;
    [Space(5f)]
    [Multiline(3)]
    [Tooltip("퀘스트의 설명")]
    public string description;

    [Space(10f)]
    [Tooltip("퀘스트에서 수행해야할 작업의 그룹")]
    public TaskGroup_Generation[] taskGroups;

    [Tooltip("퀘스트의 보상")]
    public Reward_Generation[] rewards;
    [Tooltip("퀘스트의 자동완료여부")]
    public bool useAutoComplete;
    [Tooltip("퀘스트 취소가능여부")]
    public bool isCancelable;

    [Space(10f)]
    [Header("수락,취소조건(수정중)")]
    public Condition_Generation[] acceptionConditions;
    public Condition_Generation[] cancelConditions;
}


[Serializable]
public class TaskGroup_Generation
{
    //public Task_Json[] tasks;
}

[Serializable]
public class Task_Generation
{
    public string codeName;
    public string description;
    public string actionType;
    //public Category category;
    public string[] targetsCodeName;//이것으로 타겟 검색
                                    //public InitialSuccessValue initialSuccessValue;
    public int needSuccessToComplete;
    public bool canReceiveReportsDuringCompletion;
    public int currentSuccess;
}

[Serializable]
public class Category_Generation
{
    public string codeName;
    public string displayName;
}

[Serializable]
public class Reward_Generation
{
    public string itemIconPath;
    public string description;
    public string itemCodeName;
    public int quantity;
}

[Serializable]
public class Condition_Generation
{
    public string description;
}
