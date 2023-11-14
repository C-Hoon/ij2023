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
            Debug.Log("����Ʈ �ڵ������ �����Դϴ�.");
            return;
        }
        else if (GameCore.Managers.Resource.Load<TextAsset>($"Data/Quest/{quest.codeName}") != null)
        {
            Debug.Log("�ߺ��� ����Ʈ �ڵ�����Դϴ�.");
            return;
        }
        string json = JsonUtility.ToJson(quest);
        System.IO.File.WriteAllText($"Assets/Resources/Data/Quest/{quest.codeName}.json", json);
        Debug.Log("Json������ �����Ǿ����ϴ�.");

    }
}
[Serializable]
public class Quest_Generation
{
    [Header("�⺻����")]
    [Tooltip("ī�װ�")]
    public Category_Generation category;
    [Tooltip("����Ʈ �������� �ּ�")]
    public string iconPath;
    [Tooltip("����Ʈ�� �ڵ����(��: FirstQuest_Quest, KillSlime_Quest)")]
    public string codeName;
    [Tooltip("����Ʈ�� ���ӻ� ǥ�õ� ����")]
    public string displayName;
    [Space(5f)]
    [Multiline(3)]
    [Tooltip("����Ʈ�� ����")]
    public string description;

    [Space(10f)]
    [Tooltip("����Ʈ���� �����ؾ��� �۾��� �׷�")]
    public TaskGroup_Generation[] taskGroups;

    [Tooltip("����Ʈ�� ����")]
    public Reward_Generation[] rewards;
    [Tooltip("����Ʈ�� �ڵ��ϷῩ��")]
    public bool useAutoComplete;
    [Tooltip("����Ʈ ��Ұ��ɿ���")]
    public bool isCancelable;

    [Space(10f)]
    [Header("����,�������(������)")]
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
    public string[] targetsCodeName;//�̰����� Ÿ�� �˻�
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
