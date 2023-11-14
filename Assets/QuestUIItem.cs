using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUIItem : MonoBehaviour
{
    public TextMeshProUGUI discription;
    public TextMeshProUGUI progress;
    public GameObject isCompleted;


    public void Init(string discription, int currentSuccess, int needSuccessToComplete, bool isCompleted)
    {
        this.discription.text = discription;
        this.progress.text = SetProgress(currentSuccess, needSuccessToComplete);
        this.isCompleted.SetActive(isCompleted);
    }

    string SetProgress(int current, int need )
    {
        return $"{current} / {need}";
    }
}
