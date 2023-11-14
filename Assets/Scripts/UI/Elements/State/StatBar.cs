using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStatUIEvent
{
    public int value;
}
public class StatBar : MonoBehaviour
{
    public string _path = "UI/Scene/Elements/State/Heart";
    public Define.CharacterStat _maxStat = Define.CharacterStat.MaxHp;
    public Define.CharacterStat _stat = Define.CharacterStat.Hp;
    PlayerMove playerMove;
    int MaxStat { get {return playerMove.GetStat(_maxStat); } }
    int Stat { get {return playerMove.GetStat(_stat); } }

    List<GameObject> StatList = new List<GameObject>();
    int cursor = 0;

    int statLimit = 10;

    private void Start()
    {
        //최초 초기화
        Init();
    }
    void Init()
    {
        playerMove = GameCore.Managers.Game.Player.GetComponent<PlayerMove>();
        while (statLimit > StatList.Count)
        {
            if (MaxStat > StatList.Count)
            {
                GameObject go = GameCore.Managers.Resource.Instantiate(_path, transform);
                if (go == null)
                    break;
                go.SetActive(true);
                StatList.Add(go);
            }
            else
            {
                GameObject go = GameCore.Managers.Resource.Instantiate(_path, transform);
                if (go == null)
                    break;
                go.SetActive(false);
                StatList.Add(go);
            }
        }

        for (int i = 0; i < StatList.Count; i++)
        {
            if (Stat > i)
            {
                StatList[i].GetComponent<Toggle>().isOn = true;
                cursor = i;
            }
            else
            {
                StatList[i].GetComponent<Toggle>().isOn = false;
            }
        }
    }
    private void OnEnable()
    {
        if (_stat == Define.CharacterStat.Hp)
            EventBus.Subscribe<UpdateStatUIEvent>(UpdateStat);

    }

    private void OnDisable()
    {
        if (_stat == Define.CharacterStat.Hp)
            EventBus.Unsubscribe<UpdateStatUIEvent>(UpdateStat);
    }

    public void UpdateMaxStat(UpdateStatUIEvent uiEvent)
    {
        for (int i = 0; i < StatList.Count; i++)
        {
            if(i < MaxStat)
            {
                StatList[i].SetActive(true);
            }
            else
            {
                StatList[i].SetActive(false);
            }
        }
    }
    public void UpdateStat(UpdateStatUIEvent uiEvent)
    {
        for (int i = 0; i < MaxStat; i++)
        {
            if (i < Stat)
            {
                StatList[i].GetComponent<Toggle>().isOn = true;
            }
            else
            {
                StatList[i].GetComponent<Toggle>().isOn = false;
            }
        }
    }
}
