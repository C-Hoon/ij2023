using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSlide : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    Dictionary<string, GameObject> questDictionary = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    void Init()
    {
        EventBus.Subscribe<QuestUIEvent>(CallQuestUIInfo);
    }
    public void CallQuestUIInfo(QuestUIEvent quest)
    {
        if(!questDictionary.ContainsKey(quest.name))
            questDictionary.Add(quest.name, Instantiate(prefab, transform));

        QuestUIInfo questUIInfo = questDictionary[quest.name].GetComponent<QuestUIInfo>();
        questUIInfo.Init(quest, this);
    }
    public void RemoveDictionary(string name)
    {
        Destroy(questDictionary[name]);
        questDictionary.Remove(name);
    }
}
