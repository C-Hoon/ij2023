using QuestSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    [SerializeField]
    GameObject content;

    [SerializeField]
    GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        string description;
        int currentSuccess;
        int needSuccessToComplete;
        bool isCompleted;

        var activeAchievements = GameCore.Managers.Quest.ActiveAchievements;
        foreach(var achievement in activeAchievements)
        {
            description = achievement.Description;
            currentSuccess = achievement.CurrentSuccess;
            needSuccessToComplete = achievement.NeedSuccessToComplete;
            isCompleted = achievement.IsComplete;

            QuestUIItem questUiItem = Instantiate(prefab, content.transform).GetComponent<QuestUIItem>();
            questUiItem.Init(description, currentSuccess, needSuccessToComplete, isCompleted);
        }

        var completedAchievements = GameCore.Managers.Quest.CompletedAchievements;
        foreach (var achievement in completedAchievements)
        {
            description = achievement.Description;
            currentSuccess = achievement.CurrentSuccess;
            needSuccessToComplete = achievement.NeedSuccessToComplete;
            isCompleted = achievement.IsComplete;

            QuestUIItem questUiItem = Instantiate(prefab, content.transform).GetComponent<QuestUIItem>();
            questUiItem.Init(description, currentSuccess, needSuccessToComplete, isCompleted);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
