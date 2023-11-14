using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public Define.Language language = Define.Language.ko_KR;
    

    public int totalPoint;
    public int stagePoint;
    public int stageIndex;


    public bool isPaused = false;

    GameObject player;
    PlayerStat playerStat;
    public GameObject[] Stages;
    public GameObject Player{ get { return player; } set { player = value; } }
    public PlayerStat PlayerStat { get { return playerStat; } set { playerStat = value; } }

    public bool loadMode = false;
    public void Init()
    {
    }
    public void NextStage()
    {
        //Change Stage
        if(stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerRespon();
        }
        else //Game Clear
        {
            //Player Control Lock
            Time.timeScale = 0;

            //Result UI
            Debug.Log("Clear!");

            //Restart Button UI

        }

        //Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    void PlayerRespon()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.GetComponent<PlayerMove>().VelocityZero();
    }

    public void SetPause(bool pause)
    {
        isPaused = pause;
        if (isPaused)
        {
            Time.timeScale = 0f; // 게임 시간을 멈춤
        }
        else
        {
            Time.timeScale = 1f; // 게임 시간을 다시 시작
        }
    }
    public void SaveStage()
    {
        PlayerPrefs.SetString("Stage", SceneManager.GetActiveScene().name);
    }
    public string LoadStage()
    {
        return PlayerPrefs.GetString("Stage");
    }
}
