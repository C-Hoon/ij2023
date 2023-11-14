using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUIInfo : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textComponent;
    QuestSlide questSlide;
    QuestUIEvent questUIEvent;
    public void Init(QuestUIEvent quest, QuestSlide slide)
    {
        questUIEvent = quest;
        questSlide = slide;
        SetText(questUIEvent);
        DestroyQuestUIInfo(3f);
    }

    void SetText(QuestUIEvent quest)
    {
        if (quest.isCompleted)
            textComponent.text = $"{quest.name}\n �Ϸ�";
        else
            textComponent.text = $"{quest.name}\n{quest.currentSuccess} / {quest.needSuccessToComplete}";
    }


    void DestroyQuestUIInfo(float time)
    {
        CancelInvoke("DestroyGameObject");
        Invoke("DestroyGameObject", time);
    }
    private void DestroyGameObject()
    {
        Debug.Log("DestroyGameObject!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        questSlide.RemoveDictionary(questUIEvent.name);
        // ���� ������Ʈ �ı�
        //Destroy(gameObject);
    }
}
public class QuestUIEvent
{
    public string name;
    public int currentSuccess;
    public int needSuccessToComplete;
    public bool isCompleted;
}
