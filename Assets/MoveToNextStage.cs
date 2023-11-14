using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextStage : MonoBehaviour
{
    public string sceneName; 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (GameCore.Managers.Game.Player != null)
                GameCore.Managers.Game.Player.GetComponent<PlayerMove>().playerStat.Save();
            SceneManager.LoadScene(sceneName);
        }
    }
}
